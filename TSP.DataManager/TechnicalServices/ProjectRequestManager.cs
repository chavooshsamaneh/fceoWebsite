using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace TSP.DataManager.TechnicalServices
{
    public class ProjectRequestManager : BaseObject
    {
        #region Private Managers
        ProjectManager ProjectManager;
        OwnerManager OwnerManager;
        ProjectJobHistoryManager JobHistoryManager;
        OfficeMemberManager OfficeMemberManager;
        BuildingsLicenseManager BuildingsLicenseManager;
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager;
        TSP.DataManager.NezamChartManager NezamChartManager;
        TSP.DataManager.LoginManager LoginManager;
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager;
        RegisteredNoManager RegisteredNoManager;
        PlansManager PlansManager;
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager TSObserverWorkRequestManager;
        TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager ProjectCapacityDecrementManager;

        #endregion

        #region Constructors
        public ProjectRequestManager()
            : base()
        {

        }

        public ProjectRequestManager(TSP.DataManager.TransactionManager Transact)
        {
            ProjectManager = new ProjectManager();
            OwnerManager = new OwnerManager();
            JobHistoryManager = new ProjectJobHistoryManager();
            OfficeMemberManager = new OfficeMemberManager();
            WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            NezamChartManager = new TSP.DataManager.NezamChartManager();
            LoginManager = new TSP.DataManager.LoginManager();
            WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
            BuildingsLicenseManager = new BuildingsLicenseManager();
            PlansManager = new PlansManager();
            RegisteredNoManager = new RegisteredNoManager();
            TSObserverWorkRequestManager = new TSP.DataManager.TechnicalServices.ObserverWorkRequestManager();
            ProjectCapacityDecrementManager = new TSP.DataManager.TechnicalServices.ProjectCapacityDecrementManager();

            if (Transact != null)
            {
                Transact.Add(ProjectManager);
                Transact.Add(OwnerManager);
                Transact.Add(JobHistoryManager);
                Transact.Add(OfficeMemberManager);
                Transact.Add(WorkFlowStateManager);
                Transact.Add(NezamChartManager);
                Transact.Add(LoginManager);
                Transact.Add(WorkFlowTaskManager);
                Transact.Add(BuildingsLicenseManager);
                Transact.Add(PlansManager);
                Transact.Add(RegisteredNoManager);
                Transact.Add(TSObserverWorkRequestManager);
                Transact.Add(ProjectCapacityDecrementManager);
            }
        }
        #endregion

        #region WorkFlow
        #region SendBackTask
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PrjReId"></param>
        /// <param name="CurrentTaskCode">مرحله گردش کار جاری</param>
        /// <param name="PrjId"></param>
        /// <returns>ReturnValue=0 => امکان ارسال به مرحله بعد وجود دارد</returns>
        public int CheckPermissionProjectConfirmingSendBackTask(int PrjReId, int CurrentTaskCode, ref int PrjId)
        {
            int Per = 0;
            Boolean IsShahrakSanaati = false;
            Boolean IsBonyadMaskan = false;
            Boolean NeedCoordinatorObserver = true;
            Boolean IsCharity = false;
            int DiffFoundation = 0;
            int PrjReTypeId = -1;
            this.FindByCode(PrjReId);
            if (this.Count != 1)
            {
                return (int)ErrorRequest.LoseRequestInfo;
            }
            if (!Utility.IsDBNullOrNullValue(this[0]["PrjReTypeId"]))
            {
                PrjReTypeId = Convert.ToInt32(this[0]["PrjReTypeId"]);
                if (PrjReTypeId == (int)TSP.DataManager.TSProjectRequestType.BuildingNotStarted || PrjReTypeId == (int)TSP.DataManager.TSProjectRequestType.EndProjectCertificateRequest || PrjReTypeId == (int)TSP.DataManager.TSProjectRequestType.BuildingsLicenseConfirming)
                    return 0;
                if (PrjReTypeId == (int)TSP.DataManager.TSProjectRequestType.ChangeProject)
                    if (CurrentTaskCode != (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo)
                        return 0;
            }
            if (!Utility.IsDBNullOrNullValue(this[0]["DiffFoundation"]))
                DiffFoundation = Convert.ToInt32(this[0]["DiffFoundation"]);

            int ProjectId = PrjId = (int)this[0]["ProjectId"];
            int GroupId = (int)this[0]["GroupId"];
            int StructureSkeletonId = -2;
            if (!Utility.IsDBNullOrNullValue(this[0]["StructureSkeletonId"]))
                StructureSkeletonId = Convert.ToInt32(this[0]["StructureSkeletonId"]);
            if (!Utility.IsDBNullOrNullValue(this[0]["DiscountPercentCode"]) && Convert.ToInt32(this[0]["DiscountPercentCode"]) == (int)TSDiscountPercent.Khayerin)
                IsCharity = true;
            if (!Utility.IsDBNullOrNullValue(this[0]["DiscountPercentCode"]) && Convert.ToInt32(this[0]["DiscountPercentCode"]) == (int)TSDiscountPercent.Industrial)
                IsShahrakSanaati = true;
            if (!Utility.IsDBNullOrNullValue(this[0]["DiscountPercentCode"]) && Convert.ToInt32(this[0]["DiscountPercentCode"]) == (int)TSDiscountPercent.BonyadMaskan)
                IsBonyadMaskan = true;
            #region شرط الزامی بودن ناظر هماهنگ کننده
            //**پروژه های شهرک صنعتی و بنیاد مسکن نیازی به ناظر هماهنگ کننده ندارند**
            if (IsBonyadMaskan || IsShahrakSanaati)
                NeedCoordinatorObserver = false;
            #endregion
            switch (CurrentTaskCode)
            {
                #region مراحل مربوط به طراح و نقشه
                case (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject:
                    if (Convert.ToBoolean(this[0]["DesignerSaved"]))
                        Per = CheckArchitecturePlanCondition(ProjectId, PrjReId);
                    break;

                case (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject:
                    if (Convert.ToBoolean(this[0]["DesignerSaved"]))
                        Per = CheckStructuralPlanCondition(ProjectId, PrjReId);
                    break;

                case (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject:
                    if (Convert.ToBoolean(this[0]["DesignerSaved"]))
                        Per = CheckElectricalInsPlanCondition(ProjectId, PrjReId);
                    break;

                case (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject:
                    if (Convert.ToBoolean(this[0]["DesignerSaved"]))
                        Per = CheckMechanicInsPlanCondition(ProjectId, PrjReId);
                    break;
                #endregion

                case (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo:
                    if (Per == 0)
                    {
                        if (PrjReTypeId != (int)TSP.DataManager.TSProjectRequestType.BuildingsLicenseConfirming
                            && PrjReTypeId != (int)TSP.DataManager.TSProjectRequestType.BuildingNotStarted
                            && PrjReTypeId != (int)TSP.DataManager.TSProjectRequestType.EndProjectCertificateRequest)
                            Per = CheckOwnersAgent(ProjectId);
                    }
                    ////*****************اجباری بودن فیش پنج در هزار بر اساس تنظیمات وارد شده مشخص می شود*********************** 
                    //if (IsNeed5In1000Fish(PrjReId))
                    //    if (Per == 0)
                    //        Per = Check5In1000Fiche(ProjectId, PrjReId);
                    break;

                case (int)TSP.DataManager.WorkFlowTask.SaveImplementerOfProject:
                    Per = CheckImplementer(PrjReId, ProjectId);
                    if (Per == 0)
                        Per = CheckImplementerAgent(ProjectId);
                    if (Per == 0)
                        Per = Check2In1000Fiche(ProjectId, PrjReId);

                    break;

                case (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject:
                    if (Convert.ToBoolean(this[0]["ObserverSaved"]))//**آیا گزینه ثبت درخواست بدون ناظر انتخاب شده است؟
                    {
                        Per = CheckObserver(ProjectId);
                        //**برای پروژه های خیریه مبلغ فیش نظارت چک نمی شود.مبلغ صفر است**
                        //**در صورتی که مابه تفاوت متراژ در درخواست جاری و قبلی صفر باشد نیازی به ثبت فیش در این درخواست نمی باشد
                        if (Per == 0 && !IsCharity)
                            Per = CheckObserversFiche(ProjectId, PrjReId, NeedCoordinatorObserver, DiffFoundation != 0 ? true : false);
                    }
                    break;
            }
            return Per;
        }

        #region چک کردن وضعیت از لحاظ انواع نقشه های طراح /فیش و غیره
        /// <summary>
        /// ثبت شدن طراح معماری و نقشه های آن و تایید شدن آنها چک می شود
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PrjReId"></param>
        /// <returns></returns>
        public int CheckArchitecturePlanCondition(int ProjectId, int PrjReId)
        {

            int Per = 0;
            int PlansId = -1;

            int ArchitecturalPlanType = (int)TSP.DataManager.TSPlansType.Memari;
            PlansManager.SelectByPlanType(ProjectId, ArchitecturalPlanType, 0, PrjReId);
            if (PlansManager.Count == 1)
            {
                return (int)ErrorRequest.ArchitecturePlanNotConfirm;
            }
            else
            {
                PlansManager.SelectByPlanType(ProjectId, ArchitecturalPlanType, (int)TSPlansConfirming.Confirmed, PrjReId);
                if (PlansManager.Count == 0)
                {
                    return (int)ErrorRequest.ArchitecturePlanNotConfirm;
                }
                else
                {
                    PlansId = Convert.ToInt32(PlansManager[0]["PlansId"]);
                    if (Per == 0)
                        Per = PlansManager.CheckDesigner(PlansId, ProjectId);
                    if (Per == 0)
                        Per = PlansManager.Check5PercentFiche(PlansId, ProjectId, ArchitecturalPlanType, PrjReId);
                }
            }

            return Per;
        }
        /// <summary>
        /// ثبت شدن طراح سازه و نقشه های آن و تایید شدن آنها چک می شود
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PrjReId"></param>
        /// <returns></returns>
        private int CheckStructuralPlanCondition(int ProjectId, int PrjReId)
        {
            // PlansManager PlansManager = new PlansManager();
            int Per = 0;

            int StructurePlanType = (int)TSP.DataManager.TSPlansType.Sazeh;
            PlansManager.SelectByPlanType(ProjectId, StructurePlanType, 0, PrjReId);
            if (PlansManager.Count == 1)
            {
                return (int)ErrorRequest.StructuralPlanNotConfirm;
            }
            else
            {
                PlansManager.SelectByPlanType(ProjectId, StructurePlanType, 1, PrjReId);//***نقشه در این درخواست یا درخواست های قبل
                if (PlansManager.Count == 0)
                {
                    return (int)ErrorRequest.StructuralPlanNotConfirm;
                }
                else
                {
                    int PlansId = -1;
                    PlansId = Convert.ToInt32(PlansManager[0]["PlansId"]);
                    if (Per == 0)
                        Per = PlansManager.CheckDesigner(PlansId, ProjectId);
                    if (Per == 0)
                        Per = PlansManager.Check5PercentFiche(PlansId, ProjectId, StructurePlanType, PrjReId);
                }
            }

            return Per;
        }
        /// <summary>
        /// ثبت شدن طراح برق و نقشه های آن و تایید شدن آنها چک می شود
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PrjReId"></param>
        /// <returns></returns>
        private int CheckElectricalInsPlanCondition(int ProjectId, int PrjReId)
        {
            //  PlansManager PlansManager = new PlansManager();
            int Per = 0;

            int ElectricalInsPlanType = (int)TSP.DataManager.TSPlansType.TasisatBargh;
            PlansManager.SelectByPlanType(ProjectId, ElectricalInsPlanType, 0, PrjReId);
            if (PlansManager.Count == 1)
            {
                return (int)ErrorRequest.ElectricalInsPlanNotConfirm;
            }
            else
            {
                PlansManager.SelectByPlanType(ProjectId, ElectricalInsPlanType, 1, PrjReId);
                if (PlansManager.Count == 0)
                {
                    return (int)ErrorRequest.ElectricalInsPlanNotConfirm;
                }
                else
                {
                    int PlansId = -1;
                    PlansId = Convert.ToInt32(PlansManager[0]["PlansId"]);
                    //WorkFlowStateManager = new DataManager.WorkFlowStateManager();
                    //DataTable dtWfState = WorkFlowStateManager.SelectLastState(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSPlans), PlansId);
                    //if (dtWfState.Rows.Count == 1)
                    //{
                    //    if (Convert.ToInt32(dtWfState.Rows[0]["Type"]) != (int)WorkFlowTaskType.ConfirmingAndEndProccessTask &&
                    //       Convert.ToInt32(dtWfState.Rows[0]["Type"]) != (int)WorkFlowTaskType.RejectingingAndEndProccessTask)
                    //        return (int)ErrorRequest.ElectricalInsPlanNotConfirm;
                    //}
                    //else return (int)ErrorRequest.ElectricalInsPlanNotConfirm;

                    if (Per == 0)
                        Per = PlansManager.CheckDesigner(PlansId, ProjectId);
                    if (Per == 0)
                        Per = PlansManager.Check5PercentFiche(PlansId, ProjectId, ElectricalInsPlanType, PrjReId);
                }
            }

            return Per;
        }
        /// <summary>
        /// ثبت شدن طراح مکانیک و نقشه های آن و تایید شدن آنها چک می شود
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PrjReId"></param>
        /// <returns></returns>
        private int CheckMechanicInsPlanCondition(int ProjectId, int PrjReId)
        {
            int Per = 0;

            int MechanicInsPlanType = (int)TSP.DataManager.TSPlansType.TasisatMechanic;
            PlansManager.SelectByPlanType(ProjectId, MechanicInsPlanType, 0, PrjReId);
            if (PlansManager.Count == 1)
            {
                return (int)ErrorRequest.MechanicInsPlanNotConfirm;
            }
            else
            {
                PlansManager.SelectByPlanType(ProjectId, MechanicInsPlanType, 1, PrjReId);
                if (PlansManager.Count == 0)
                {
                    return (int)ErrorRequest.MechanicInsPlanNotConfirm;
                }
                else
                {
                    int PlansId = -1;
                    PlansId = Convert.ToInt32(PlansManager[0]["PlansId"]);
                    if (Per == 0)
                        Per = PlansManager.CheckDesigner(PlansId, ProjectId);
                    if (Per == 0)
                        Per = PlansManager.Check5PercentFiche(PlansId, ProjectId, MechanicInsPlanType, PrjReId);
                }
            }

            return Per;
        }
        #endregion

        #region  چک کردن شرایط مربوط به سربرگ های اطلاعات پروژه
        private int CheckMainRegisteredNo(int ProjectId)
        {

            int Per = 0;

            if (!RegisteredNoManager.HaveMain(ProjectId))
                Per = (int)ErrorRequest.NoMainRegisteredNo;

            return Per;
        }

        private int CheckOwnersAgent(int ProjectId)
        {
            int Per = 0;
            OwnerManager.FindOwnerAgent(ProjectId);
            if (OwnerManager.Count == 0)
                Per = (int)ErrorRequest.NoOwnerAgent;

            return Per;
        }
        #endregion

        private bool IsNeed5In1000Fish(int PrjReId)
        {
            bool Result = false;
            SettingManager SettingManager = new SettingManager();
            SettingManager.FindActiveSetting();
            if (SettingManager.Count == 1)
            {
                if (Convert.ToBoolean(SettingManager[0]["IsNeed5In1000Fish"]))
                {
                    double Foundation = -1;
                    int Step = -1;

                    this.FindByCode(PrjReId);
                    Foundation = Convert.ToDouble(this[0]["Foundation"]);


                    Step = Convert.ToInt32(this[0]["MaxStageNum"]);

                    if (Foundation >= Convert.ToInt32(SettingManager[0]["Foundation"]) || Step >= Convert.ToInt32(SettingManager[0]["Step"]))
                        Result = true;
                }
            }
            return Result;
        }

        private int Check5In1000Fiche(int ProjectId, int PrjReId)
        {
            int Per = 0;
            decimal Amount = 0;
            AccountingManager AccountingManager = new AccountingManager();
            //OwnerManager OwnerManager = new OwnerManager();

            OwnerManager.FindOwnerAgent(ProjectId);
            if (OwnerManager.Count != 1)
                return (int)ErrorRequest.NoOwnerAgent;
            int OwnerId = Convert.ToInt32(OwnerManager[0]["OwnerId"].ToString());

            AccountingManager.FindByTableTypeIdAndAccType(OwnerId, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSOwner),
                (int)TSP.DataManager.TSAccountingAccType._5In1000);

            if (AccountingManager.Count == 0)
                Per = (int)ErrorRequest.No5In1000Fiche;
            else
            {
                for (int i = 0; i < AccountingManager.Count; i++)
                {
                    if (Convert.ToInt32(AccountingManager[i]["Status"]) != (int)TSAccountingStatus.Payment)
                        return (int)ErrorRequest.NoPay5In1000Fish;
                    Amount += Convert.ToDecimal(AccountingManager[i]["Amount"]);
                }

                decimal OwnerPrice;
                if (Utility.TSProject_IsBasedOnStep())
                    OwnerPrice = Get5In1000PriceByStep(PrjReId);
                else
                    OwnerPrice = Get5In1000PriceByRequest(PrjReId);
                if (Amount != OwnerPrice)
                    Per = (int)ErrorRequest.AmountOf5In1000FicheNotMatch;
            }

            return Per;
        }

        private int CheckImplementer(int PrjReId, int ProjectId)
        {
            int Per = 0; if (IsNeed5In1000Fish(PrjReId))
            {
                Project_ImplementerManager ImplementerManager = new Project_ImplementerManager();
                ProjectManager.FindByProjectId(ProjectId);

                ImplementerManager.FindImpMother(ProjectId);
                if (ImplementerManager.Count == 0)
                    Per = (int)ErrorRequest.NoImplementer;
            }
            return Per;
        }

        private int CheckImplementerAgent(int ProjectId)
        {
            Project_ImplementerManager ImplementerManager = new Project_ImplementerManager();
            ImplementerAgentManager ImpAgentManager = new ImplementerAgentManager();
            int Per = 0;
            ImplementerManager.FindImpMother(ProjectId);
            if (ImplementerManager.Count > 0)
            {
                ImpAgentManager.FindByPrjImpId(Convert.ToInt32(ImplementerManager[0]["PrjImpId"]));
                if (ImpAgentManager.Count == 0)
                    Per = (int)ErrorRequest.NoImplementerAgent;

            }

            return Per;
        }

        private int CheckObserver(int ProjectId)
        {
            int Per = 0;
            Project_ObserversManager ProjectObserversManager = new Project_ObserversManager();
            ProjectObserversManager.FindActivesByProjectId(ProjectId);

            if (ProjectObserversManager.Count == 0)
                Per = (int)ErrorRequest.NoObserver;

            return Per;
        }

        private int CheckObserversMother(int ProjectId)
        {
            int Per = 0;
            Project_ObserversManager ProjectObserversManager = new Project_ObserversManager();
            ProjectObserversManager.FindObsMother(ProjectId);

            if (ProjectObserversManager.Count == 0)
                Per = (int)ErrorRequest.NoObserversMother;

            return Per;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProjectId">آدی پروژه:کد پروژه</param>
        /// <param name="PrjReId">آی دی درخواست</param>
        /// <param name="NeedCoordinatorObserver">آیا نیاز به ناظر هماهنگ کننده دارد؟</param>
        /// <returns></returns>
        private int CheckObserversFiche(int ProjectId, int PrjReId, Boolean NeedCoordinatorObserver, Boolean CheckHasFishInCurrentRequest)
        {
            int Per = 0;
            decimal Amount = 0;
            if (!CheckHasFishInCurrentRequest)
                return Per;
            Project_ObserversManager Project_ObserversManager = new TechnicalServices.Project_ObserversManager();
            Project_ObserversManager.FindObsMother(ProjectId);
            if (Project_ObserversManager.Count == 0 && NeedCoordinatorObserver)
                return (int)ErrorRequest.NoObserversMother;
            bool Has100PersentObsFish = false;
            bool Has5PersentObsFish_Instalation = false;
            bool Has5PersentObsFish_Structure = false;
            AccountingManager AccountingManager = new AccountingManager();
            AccountingManager.FindByTableTypeIdAndAccType(PrjReId,
               TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers), (int)TSP.DataManager.TSAccountingAccType.ObserversFiche);
            if (AccountingManager.Count != 0)
            {
                Has100PersentObsFish = true;
                for (int i = 0; i < AccountingManager.Count; i++)
                {
                    if (Convert.ToInt32(AccountingManager[i]["Status"]) != (int)TSAccountingStatus.Payment)
                        return (int)ErrorRequest.NotPayObserverFish;
                    Amount += Convert.ToDecimal(AccountingManager[i]["Amount"]);
                }
            }

            AccountingManager.FindByTableTypeIdAndAccType(PrjReId,
         TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers), (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation);
            if (AccountingManager.Count != 0)
            {
                Has5PersentObsFish_Instalation = true;
                for (int i = 0; i < AccountingManager.Count; i++)
                {
                    if (Convert.ToInt32(AccountingManager[i]["Status"]) != (int)TSAccountingStatus.Payment)
                        return (int)ErrorRequest.NotPayObserverFish;
                    Amount += Convert.ToDecimal(AccountingManager[i]["Amount"]);
                }
            }

            AccountingManager.FindByTableTypeIdAndAccType(PrjReId,
                TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers), (int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure);
            if (AccountingManager.Count != 0)
            {
                Has5PersentObsFish_Structure = true;
                for (int i = 0; i < AccountingManager.Count; i++)
                {
                    if (Convert.ToInt32(AccountingManager[i]["Status"]) != (int)TSAccountingStatus.Payment)
                        return (int)ErrorRequest.NotPayObserverFish;
                    Amount += Convert.ToDecimal(AccountingManager[i]["Amount"]);
                }
            }
            if (!Has100PersentObsFish && !Has5PersentObsFish_Instalation && !Has5PersentObsFish_Structure)
            {
                Per = (int)ErrorRequest.NoObserversFiche;
                return Per;
            }
            return Per;
        }

        protected int GetMajorId(int ObserversTypeId)
        {
            switch (ObserversTypeId)
            {
                case (int)TSP.DataManager.TSObserversType.Mapping:
                    return (int)TSP.DataManager.MainMajors.Mapping;

                case (int)TSP.DataManager.TSObserversType.Memari:
                    return (int)TSP.DataManager.MainMajors.Architecture;

                case (int)TSP.DataManager.TSObserversType.Sazeh:
                    return (int)TSP.DataManager.MainMajors.Civil;

                case (int)TSP.DataManager.TSObserversType.TasisatBargh:
                    return (int)TSP.DataManager.MainMajors.Electronic;

                case (int)TSP.DataManager.TSObserversType.TasisatMechanic:
                    return (int)TSP.DataManager.MainMajors.Mechanic;

                default:
                    return -1;

            }
        }

        private int CheckBuildingsLicense(int ProjectId)
        {
            int Per = 0;
            if (BuildingsLicenseManager == null)
                BuildingsLicenseManager = new BuildingsLicenseManager();
            BuildingsLicenseManager.FindByProjectId(ProjectId);
            if (BuildingsLicenseManager.Count == 0)
                Per = (int)ErrorRequest.NoBuildingsLicense;

            return Per;
        }
        #endregion
        #region  SendDocToNextStep

        public int DoNextTaskOfConfirming(int PrjReId, int CurrentUserId, int CurrentCounId, int ProjectId)
        {
            int ProjectStatusId = -1;
            int Per = UpdateProjectRequestConfirmingStatus(PrjReId, CurrentUserId, (int)TSProjectRequestConfirming.Confirmed, ref ProjectStatusId);
            if (Per == 0)
                Per = CopyPrjReIntoProject(CurrentUserId, (TSProjectStatus)ProjectStatusId);
            if (Per == 0)
            {
                #region آزاد سازی کار طراحان پروژه
                FreeProjectDesignerWorkCount(ProjectId, CurrentUserId);

                CapacityCalculations CapacityCalculations = new CapacityCalculations();
                DataTable dt = ProjectCapacityDecrementManager.selectDesignerForFreeWorkCount(ProjectId);
                int MeId = -2;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    MeId = Convert.ToInt32(dt.Rows[i]["MeId"]);

                    int per = CapacityCalculations.UpdateWorkRequestCapacityData(TSObserverWorkRequestManager, ProjectCapacityDecrementManager, MeId, CurrentUserId, ProjectId, -2, false, TSP.DataManager.TSProjectIngridientType.Designer, null, false, false, false);
                    if (per != 0)
                    {
                        if (per == (int)CapacityCalculations.CapacityErr.CanNotFindInfo && (Convert.ToBoolean(dt.Rows[i]["SaveWithOutCondition"]) || Convert.ToBoolean(dt.Rows[i]["IsWorkFree"])))
                            continue;
                        else
                        {
                            Per = (int)ErrorRequest.ErrorInUpdatingDesignerWorkCount;
                            break;
                        }
                    }
                }
                #endregion
            }
            return Per;
        }

        public void FreeProjectDesignerWorkCount(int ProjectId, int UserId)
        {
            SqlCommand cmd = new SqlCommand("FreeProjectDesignerWorkCount", this.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Connection = this.Connection;
                if (this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();
                cmd.Transaction = this.Transaction;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.Parameters.AddWithValue("@DateofToday", Utility.GetDateOfToday());
                cmd.ExecuteNonQuery();
                if (this.Transaction == null)
                    this.Connection.Close();


            }
            finally
            {
                cmd.Connection = null;
                cmd = null;

            }

        }
        public int DoNextTaskOfRejecting(int PrjReId, int CurrentUserId)
        {
            int ProjectStatusId = -1;
            int Per = UpdateProjectRequestConfirmingStatus(PrjReId, CurrentUserId, (int)TSProjectRequestConfirming.NotConfirmed, ref ProjectStatusId);
            return Per;
        }

        public int UpdateProjectRequestConfirmingStatus(int PrjReId, int CurrentUserId, int ConfirmingStatus, ref int ProjectStatusId)
        {
            int Per = 0;
            this.FindByCode(PrjReId);
            if (this.Count > 0)
            {
                #region تنظیم وضعیت پروژه
                if (ConfirmingStatus == (int)TSProjectRequestConfirming.Confirmed)
                {
                    switch ((TSProjectRequestType)this[0]["PrjReTypeId"])
                    {
                        case TSProjectRequestType.InsertProject:
                            ProjectStatusId = (int)TSProjectStatus.Start;
                            break;
                        case TSProjectRequestType.EndProjectCertificateRequest:
                            ProjectStatusId = (int)TSProjectStatus.End;
                            break;
                        case TSProjectRequestType.AdditionalStageRequest://**اضافه اشکوب
                            ProjectStatusId = (int)TSProjectStatus.Start;
                            break;
                        case TSProjectRequestType.IncreaseBuildingAreaRequest://**توسعه بنا                                                                    
                            ProjectStatusId = (int)TSProjectStatus.Start;
                            break;
                        case TSProjectRequestType.BuildingNotStarted:
                            ProjectStatusId = (int)TSProjectStatus.BuildingNotStarted;
                            break;
                        case TSProjectRequestType.ChangeProject:
                        case TSProjectRequestType.BuildingsLicenseConfirming:
                            //*****درخواست تغییرات  و درخواست پروانه ساخت هیچ تغییری در وضعیت پروژه ایجاد نمی کند
                            ProjectStatusId = Convert.ToInt32(this[0]["ProjectStatusId"]);
                            break;
                        default:
                            ProjectStatusId = Convert.ToInt32(this[0]["ProjectStatusId"]);
                            break;
                    }

                }
                else
                {
                    ProjectStatusId = Convert.ToInt32(this[0]["ProjectStatusId"]);
                }
                #endregion
                this[0].BeginEdit();
                this[0]["ProjectStatusId"] = ProjectStatusId;
                this[0]["IsConfirmed"] = ConfirmingStatus;
                this[0]["UserId"] = CurrentUserId;
                this[0]["ModifiedDate"] = DateTime.Now;
                this[0].EndEdit();
                if (this.Save() > 0)
                    Per = 0;
                else
                    Per = (int)ErrorWFNextStep.Error;
            }
            else
                Per = (int)ErrorRequest.LoseRequestInfo;

            return Per;
        }

        private int CopyPrjReIntoProject(int CurrentUserId, TSProjectStatus ProjectStatusId)
        {
            int Per = 0;
            ProjectManager.FindByProjectId(Convert.ToInt32(this[0]["ProjectId"]));
            if (ProjectManager.Count == 1)
            {
                ProjectManager[0].BeginEdit();
                ProjectManager[0]["ProjectStatusId"] = (int)ProjectStatusId;
                ProjectManager[0]["GroupId"] = this[0]["GroupId"];
                ProjectManager[0]["Foundation"] = this[0]["Foundation"];
                ProjectManager[0]["Area"] = this[0]["Area"];
                ProjectManager[0]["RecessArea"] = this[0]["RecessArea"];
                ProjectManager[0]["RemainArea"] = this[0]["RemainArea"];
                ProjectManager[0]["DocumentArea"] = this[0]["DocumentArea"];
                ProjectManager[0]["AgentId"] = this[0]["AgentId"];
                ProjectManager[0]["DiscountPercentId"] = this[0]["DiscountPercentId"];
                ProjectManager[0]["ProjectName"] = this[0]["ProjectName"];
                ProjectManager[0]["FileNo"] = this[0]["FileNo"];
                ProjectManager[0]["FileDate"] = this[0]["FileDate"];
                ProjectManager[0]["UsageId"] = this[0]["UsageId"];
                ProjectManager[0]["ReconstructionCode"] = this[0]["ReconstructionCode"];
                ProjectManager[0]["CitId"] = this[0]["CitId"];
                ProjectManager[0]["MunId"] = this[0]["MunId"];
                ProjectManager[0]["Address"] = this[0]["Address"];
                ProjectManager[0]["Date"] = this[0]["Date"];
                ProjectManager[0]["ComputerCode"] = this[0]["ComputerCode"];
                ProjectManager[0]["ArchiveNo"] = this[0]["ArchiveNo"];
                ProjectManager[0]["OwnershipTypeId"] = this[0]["OwnershipTypeId"];
                ProjectManager[0]["DesignerSaved"] = this[0]["DesignerSaved"];
                ProjectManager[0]["OwnerFullName"] = this[0]["OwnerFullName"];
                ProjectManager[0]["MainRegisterNo"] = this[0]["MainRegisterNo"];
                ProjectManager[0]["MainRegion"] = this[0]["MainRegion"];
                ProjectManager[0]["MainSection"] = this[0]["MainSection"];
                ProjectManager[0]["FileUrlBuildingLicence"] = this[0]["FileUrlBuildingLicence"];
                ProjectManager[0]["FileUrlTechnicalBooklet"] = this[0]["FileUrlTechnicalBooklet"];
                ProjectManager[0]["MaxStageNum"] = this[0]["MaxStageNum"];
                ProjectManager[0]["FileUrlBuildingCertificate"] = this[0]["FileUrlBuildingCertificate"];
                ProjectManager[0]["BuildingCertificateStartDate"] = this[0]["BuildingCertificateStartDate"];
                ProjectManager[0]["BuildingCertificateExpirDate"] = this[0]["BuildingCertificateExpirDate"];
                ProjectManager[0]["BuildingCertificateNum"] = this[0]["BuildingCertificateNum"];
                ProjectManager[0]["FoundationMixSkeleton"] = this[0]["FoundationMixSkeleton"];
                ProjectManager[0]["RoofTypeId"] = this[0]["RoofTypeId"];
                ProjectManager[0]["StructureSkeletonId"] = this[0]["StructureSkeletonId"];
                ProjectManager[0]["FileUrlEndProject"] = this[0]["FileUrlEndProject"];
                ProjectManager[0]["EndProjectStartDate"] = this[0]["EndProjectStartDate"];
                ProjectManager[0]["EndProjectExpirDate"] = this[0]["EndProjectExpirDate"];
                ProjectManager[0]["EndProjectNum"] = this[0]["EndProjectNum"];
                ProjectManager[0]["BuldingCheckDate"] = this[0]["BuldingCheckDate"];
                ProjectManager[0]["FileURLBuldingCheck"] = this[0]["FileURLBuldingCheck"];

                ProjectManager[0]["UserId"] = CurrentUserId;
                ProjectManager[0]["ModifiedDate"] = DateTime.Now;

                ProjectManager[0].EndEdit();

                if (ProjectManager.Save() > 0)
                {
                    ProjectManager.DataTable.AcceptChanges();
                    Per = 0;
                }
                else
                    Per = (int)ErrorWFNextStep.Error;
            }
            else
                Per = (int)ErrorRequest.LoseRequestInfo;

            return Per;
        }

        private int InsertWorkFlowState(int TableId, int CurrentUserId)
        {
            int TaskCode = (int)TSP.DataManager.WorkFlowTask.SaveBuildingsLicensesInfo;
            int TaskId = GetTaskId(TaskCode);

            if (TaskId < 0)
                return TaskId;

            int CurrentNmcId = FindNmcId(TaskId, CurrentUserId);
            if (CurrentNmcId < 0)
                return CurrentNmcId;

            int WfStart = WorkFlowStateManager.StartWorkFlow(TableId, TaskCode, CurrentNmcId, CurrentUserId, (int)WorkFlowStateNmcIdType.NmcId);
            if (WfStart > 0)
                return WfStart;
            else
                return (int)ErrorWFNextStep.Error;
        }

        private int GetTaskId(int TaskCode)
        {
            WorkFlowTaskManager.FindByTaskCode(TaskCode);
            if (WorkFlowTaskManager.Count == 1)
                return Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);
            else
                return (int)ErrorRequest.LoseRequestInfo;
        }

        private int FindNmcId(int TaskId, int CurrentUserId)
        {
            int NmcId = -1;
            NmcId = NezamChartManager.FindNmcId(CurrentUserId, TaskId, LoginManager);
            if (NmcId > 0)
            {
                return NmcId;
            }
            else
            {
                return (int)TSP.DataManager.ErrorWFNextStep.UserIsNotInNezamChart;
            }
        }

        public int CheckConditionsForNextStepOfConfirming(int SelectedTaskId, int PrjReId)
        {
            int Per = 0;
            Boolean IsBonyadMaskan = false;
            WorkFlowTaskManager WorkFlowTaskManager = new WorkFlowTaskManager();
            int TaskCode = -1;

            this.FindByCode(PrjReId);
            if (this.Count == 1)
            {
                int PrjReTypeId = -1;
                if (!Utility.IsDBNullOrNullValue(this[0]["PrjReTypeId"]))
                {
                    PrjReTypeId = Convert.ToInt32(this[0]["PrjReTypeId"]);
                    if (PrjReTypeId == (int)TSP.DataManager.TSProjectRequestType.ChangeProject)
                        return 0;
                }
                if (!Utility.IsDBNullOrNullValue(this[0]["DiscountPercentCode"]) && Convert.ToInt32(this[0]["DiscountPercentCode"]) == (int)TSDiscountPercent.BonyadMaskan)
                    IsBonyadMaskan = true;
                int ProjectId = (int)this[0]["ProjectId"];
                WorkFlowTaskManager.FindByCode(SelectedTaskId);
                if (WorkFlowTaskManager.Count == 1)
                {
                    TaskCode = int.Parse(WorkFlowTaskManager[0]["TaskCode"].ToString());
                }

                switch (TaskCode)
                {

                    case (int)TSP.DataManager.WorkFlowTask.SaveProjectBaseInfo:

                        break;

                    case (int)TSP.DataManager.WorkFlowTask.SaveArchitecturalPlanOfProject:

                        break;

                    case (int)TSP.DataManager.WorkFlowTask.SaveStructurePlanOfProject:
                    case (int)TSP.DataManager.WorkFlowTask.SaveMechanicInstallationPlanOfProject:
                    case (int)TSP.DataManager.WorkFlowTask.SaveElectricalInstalationPlanOfProject:
                    case (int)TSP.DataManager.WorkFlowTask.SaveStructureAndInstallationPlanOfProject:

                        if (Convert.ToBoolean(this[0]["DesignerSaved"]))
                            Per = CheckArchitecturePlanCondition(ProjectId, PrjReId);
                        break;

                    case (int)TSP.DataManager.WorkFlowTask.SaveObserverOfProject:
                        ////*****************اجباری بودن فیش پنج در هزار بر اساس تنظیمات وارد شده مشخص می شود***********************                     
                        Per = CheckImplementer(PrjReId, ProjectId);
                        break;

                    case (int)TSP.DataManager.WorkFlowTask.ProjectObserverAcceptingTask:
                        Per = CheckObserver(ProjectId);
                        break;

                    case (int)TSP.DataManager.WorkFlowTask.SaveImplementerOfProject:

                        break;

                    case (int)TSP.DataManager.WorkFlowTask.ControlManagerConfirmingImplementerOfProject:
                        Per = CheckImplementer(PrjReId, ProjectId);
                        break;

                    case (int)TSP.DataManager.WorkFlowTask.TechnicalServiceManagerConfirmingProject:
                        Per = CheckArchitecturePlanCondition(ProjectId, PrjReId);
                        if (Per == 0)
                            Per = CheckImplementer(PrjReId, ProjectId);
                        if (Per == 0)
                            Per = CheckObserver(ProjectId);
                        break;
                    case (int)TSP.DataManager.WorkFlowTask.ConfirmingProjectAndEndProccess:
                        if (Convert.ToBoolean(this[0]["DesignerSaved"]))
                            Per = CheckArchitecturePlanCondition(ProjectId, PrjReId);
                        if (Per == 0 && !IsBonyadMaskan && Convert.ToBoolean(this[0]["ObserverSaved"]))
                            Per = CheckObserver(ProjectId);
                        if (Per == 0)
                        {
                            //درخواست نقشه در جریان نداشته باشد
                            PlansManager.SelectTSPlansByProjectAndRequest(ProjectId, -1, 0, PrjReId);
                            if (PlansManager.Count > 0)
                            {
                                Per = (int)ErrorWFNextStep.CanNotConfirmProjectAllPlansNotConfirm;
                            }
                        }
                        break;
                }
            }
            else
            {
                Per = (int)ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }

        public int CheckConditionsForNextStepOfBuildingsLicenseConfirming(int PrjReId)
        {
            int Per = 0;
            this.FindByCode(PrjReId);
            if (this.Count == 1)
            {
                int ProjectId = Convert.ToInt32(this[0]["ProjectId"]);
                Per = CheckBuildingsLicense(ProjectId);
            }
            else
                Per = (int)ErrorRequest.LoseRequestInfo;

            return Per;
        }
        #endregion
        #endregion
        #region توابع محاسبه هزینه دستمزدها
        /// <summary>
        /// محاسبه هزینه دفترچه فنی ملکی - پنج در هزار : زیربنای ملک * 0.005 * هزینه بدست آمده از جدول تعرفه ها بر اساس گروه ساختمانی و تعداد طبقات
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public decimal Get5In1000PriceByRequest(int PrjReId)
        {
            PriceArchiveManager PriceArchiveManager = new PriceArchiveManager();
            PriceArchiveStructureItemsManager PriceArchiveStructureItemsManager = new PriceArchiveStructureItemsManager();
            decimal Price = 0;

            this.FindByCode(PrjReId);
            if (this.Count > 0)
            {
                int GroupId = Convert.ToInt32(this[0]["GroupId"]);
                double Foundation = Convert.ToDouble(this[0]["Foundation"]) * 0.005;
                int Step = Convert.ToInt32(this[0]["MaxStageNum"]);
                PriceArchiveManager.FindLastPriceArchive();
                if (PriceArchiveManager.Count > 0)
                {
                    int PriceArchiveId = Convert.ToInt32(PriceArchiveManager[0]["PriceArchiveId"]);
                    PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, Step);
                    if (PriceArchiveStructureItemsManager.Count == 0)
                        PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1);
                    if (PriceArchiveStructureItemsManager.Count > 0)
                        Price = Convert.ToDecimal(PriceArchiveStructureItemsManager[0]["BuildCost"]) * Convert.ToDecimal(Foundation);
                    //Price = Convert.ToDecimal(Convert.ToDouble(Price) * Foundation);
                }
            }
            return Price;
        }

        public decimal Get5In1000PriceByStep(int PrjReId)
        {
            PriceArchiveManager PriceArchiveManager = new PriceArchiveManager();
            PriceArchiveStructureItemsManager PriceArchiveStructureItemsManager = new PriceArchiveStructureItemsManager();

            decimal Price = 0;

            this.FindByCode(PrjReId);
            if (this.Count > 0)
            {
                double Foundation = Convert.ToDouble(this[0]["Foundation"]) * 0.005;
                int Step = Convert.ToInt32(this[0]["MaxStageNum"]);
                PriceArchiveManager.FindLastPriceArchive();
                if (PriceArchiveManager.Count > 0)
                {
                    int PriceArchiveId = Convert.ToInt32(PriceArchiveManager[0]["PriceArchiveId"]);
                    PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, -1, Step);
                    if (PriceArchiveStructureItemsManager.Count > 0)
                        Price = Convert.ToDecimal(PriceArchiveStructureItemsManager[0]["BuildCost"]) * Convert.ToDecimal(Foundation);
                }
            }
            return Price;
        }

        #region توابع محاسبه فیش نظارت
        //**************************
        /// <summary>
        ///***** تابع محاسبه فیش نظارت قبل از ارجاع کار نظارت******
        ///محاسبه هزینه دستمزد بدون تعیین ناظرین و با توحه به ترکیب آنها   :
        /// </summary>
        /// <param name="ProjectId"></param>
        /// /// <param name="PrjReqId"></param>
        /// /// <param name="ObserverFishPersent"></param>
        /// <param name="ObserveringredientsList"></param>       
        /// <returns>Price </returns>
        public ArrayList GetObserversPriceByingredientsPercentList(int ProjectId, int PrjReqId, int ObserverFishPersent = 100, DataTable dtObserveringredients = null)
        {
            return GetObserversPriceByingredientsPercentListNew(ProjectId, PrjReqId, ObserverFishPersent, dtObserveringredients);
        }
        /// <summary>
        ///براساس فرمول قدیم قبل از 1399-12-01
        ///فیش بدون ناظر
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PrjReqId"></param>
        /// <param name="ObserverFishPersent"></param>
        /// <param name="dtObserveringredients"></param>
        /// <returns></returns>
        private ArrayList GetObserversPriceByingredientsPercentListOld(int ProjectId, int PrjReqId, int ObserverFishPersent = 100, DataTable dtObserveringredients = null)
        {
            PriceArchiveManager PriceArchiveManager = new PriceArchiveManager();
            PriceArchiveStructureItemsManager PriceArchiveStructureItemsManager = new PriceArchiveStructureItemsManager();
            PriceArchiveStructureItemDetailManager PriceArchiveStructureItemsDetailManager = new PriceArchiveStructureItemDetailManager();
            int WagePercent;
            int PreFoundation = 0;
            double Foundation = 0;
            double FoundationProject = 0;
            double FoundationCalculation = 0;
            int PreMaxStageNum = 0;
            decimal ObsPrice = 0;
            double SumDecimalTotal = 0;
            int GroupId = -1;
            int Step = -1;
            int StructureSkeletonId = -1;
            Boolean _IsExtraFloor = false;

            decimal ExteraFloorRatio = 1;
            bool firstObsCevil = false;
            Boolean IsObsMother = false;

            ArrayList ArrayResult = new ArrayList();
            ArrayResult.Add(0);
            ArrayResult.Add(-1);

            #region بدست آوردن اطلاعات پروژه بر اساس درخواست جاری

            this.FindByCode(PrjReqId);
            if (this.Count <= 0)
            {
                return ArrayResult;
            }
            WagePercent = Convert.ToInt32(this[0]["WagePercent"]);
            FoundationProject = Foundation = Convert.ToDouble(this[0]["FoundationRequest"]);//ملاک متراژ در خواست است نه متراژ کل پروژه

            Step = Convert.ToInt32(this[0]["MaxStageNum"]);
            #region نوع اسکلت            
            if (!Utility.IsDBNullOrNullValue(this[0]["StructureSkeletonId"]))
                StructureSkeletonId = Convert.ToInt32(this[0]["StructureSkeletonId"]);

            GroupId = Convert.ToInt32(this[0]["GroupId"]);
            int DiscountPercentId = Convert.ToInt32(this[0]["DiscountPercentId"]);

            if (GroupId == (int)TSP.DataManager.TSStructureGroups.A && StructureSkeletonId == -1)
            {
                return ArrayResult;
            }
            if (GroupId != (int)TSP.DataManager.TSStructureGroups.A || StructureSkeletonId != (int)TSStructureSkeleton.Ajory)
                StructureSkeletonId = -1;

            #endregion
            #region اضافه اشکوب و توسعه بنا
            //متراز در درخواست های قبلی که به تایید و پایان بررسی رسیده را بدست می آوریم و با متراژ فعلی مقایسه می کنیم
            DataTable dtPreRrjRest = this.SelectPreviousProjectRequestStageAndFoundation(ProjectId, PrjReqId);
            if (dtPreRrjRest.Rows.Count == 1)
            {
                PreFoundation = Convert.ToInt32(dtPreRrjRest.Rows[0]["Foundation"]);
                PreMaxStageNum = Convert.ToInt32(dtPreRrjRest.Rows[0]["MaxStageNum"]);
                if (Foundation - PreFoundation > 0 && Convert.ToInt32(this[0]["tblPrjProjectStatusId"]) == (int)TSP.DataManager.TSProjectStatus.End)
                {
                    _IsExtraFloor = true;
                }
                if (Foundation - PreFoundation > 0)
                    Foundation = Foundation - PreFoundation;
                if (_IsExtraFloor)
                    ExteraFloorRatio = 1.5m;
            }
            #endregion
            #endregion
            for (int i = 0; i < dtObserveringredients.Rows.Count; i++)
            {
                #region متراژ برای هر ناظر
                //فرمول درصد متراژ هر ناظر در هر درخواست باید با در نظر گرفتن جمع خورده ها همانند صفحه ی اختصاص ناظران پیاده سازی شود 
                //Employee_TechnicalServices_Project_ObserverSelected ===>  FunctionA2()
                //حتما صفحه ی بالا را در نظر بگیرید
                double PercentOfPrjFundation = Convert.ToDouble(dtObserveringredients.Rows[i]["FoundationPercentList"].ToString().Split(',')[0]) / 100;

                SumDecimalTotal = Math.Round(((Foundation * PercentOfPrjFundation) - Math.Floor(Foundation * PercentOfPrjFundation)) + SumDecimalTotal, 1);

                if (SumDecimalTotal == 1 && FoundationProject > 100)
                {
                    FoundationCalculation = Math.Floor(((FoundationProject <= 100 ? 100 : Foundation) * PercentOfPrjFundation)) + SumDecimalTotal;
                    SumDecimalTotal = 0;
                }
                else
                {
                    FoundationCalculation = Math.Floor((FoundationProject <= 100 ? 100 : Foundation) * PercentOfPrjFundation);
                }

                #endregion

                if (!firstObsCevil &&
                    ((int)TSP.DataManager.MainMajors.Civil == Convert.ToInt32(dtObserveringredients.Rows[i]["MajorIdList"].ToString().Split(',')[0]) || (int)TSP.DataManager.MainMajors.Architecture == Convert.ToInt32(dtObserveringredients.Rows[i]["MajorIdList"].ToString().Split(',')[0])))
                {
                    IsObsMother = true;//اولین ناظر سازه/معماری را هماهنگ کننده در نظر می گیریم
                    firstObsCevil = true;
                }

                PriceArchiveManager.FindLastPriceArchive();//آخرین تعرفه فعال را تعرفه جاری می دانیم
                if (PriceArchiveManager.Count > 0)
                {
                    //TSStructureSkeleton.Banaee
                    int PriceArchiveId = Convert.ToInt32(PriceArchiveManager[0]["PriceArchiveId"]);
                    PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, Step, StructureSkeletonId);
                    if (PriceArchiveStructureItemsManager.Count == 0)
                    {
                        PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, StructureSkeletonId);
                        if (PriceArchiveStructureItemsManager.Count == 0)
                            PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, -1);
                    }
                }
                if (PriceArchiveStructureItemsManager.Count > 0)
                {
                    PriceArchiveStructureItemsDetailManager.FindByItemIdAndTypeIdAndMjId(Convert.ToInt32(PriceArchiveStructureItemsManager[0]["ItemId"]), (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision, Convert.ToInt32(dtObserveringredients.Rows[i]["MajorIdList"].ToString().Split(',')[0]));
                    if (PriceArchiveStructureItemsDetailManager.Count > 0)
                    {
                        #region محاسبه دستمزد ناظر هماهنگ کننده
                        decimal CoordinatorPrice = 0;
                        if (IsObsMother)
                        { //متراژ ناظر هماهنگ کننده به اندازه متراژ کل در خواست است                           
                            CoordinatorPrice = Convert.ToDecimal(((FoundationProject <= 100 ? (100) : Foundation * WagePercent / 100))) * Convert.ToDecimal(PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]);
                            IsObsMother = false;
                        }
                        #endregion
                        ObsPrice += ExteraFloorRatio * Math.Floor(Convert.ToDecimal(FoundationCalculation) * WagePercent / 100) * Convert.ToDecimal(PriceArchiveStructureItemsDetailManager[0]["Price"]);
                        ObsPrice += ExteraFloorRatio * CoordinatorPrice;
                        ArrayResult[1] = Convert.ToDecimal(PriceArchiveStructureItemsDetailManager[0]["ItemDetailId"]);

                    }
                }


            }
            ObsPrice = ObsPrice * ObserverFishPersent / 100;
            ArrayResult[0] = ObsPrice;
            return ArrayResult;
        }
        /// <summary>
        ///***** تابع محاسبه فیش نظارت قبل از ارجاع کار نظارت******براساس قوانین جدید اضافه اشکوب
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PrjReqId"></param>
        /// <param name="ObserverFishPersent"></param>
        /// <param name="dtObserveringredients"></param>
        /// <returns></returns>
        private ArrayList GetObserversPriceByingredientsPercentListNew(int ProjectId, int PrjReqId, int ObserverFishPersent = 100, DataTable dtObserveringredients = null)
        {
            AccountingManager AccountingManager = new AccountingManager();
            PriceArchiveManager PriceArchiveManager = new PriceArchiveManager();
            PriceArchiveStructureItemsManager PriceArchiveStructureItemsManager = new PriceArchiveStructureItemsManager();
            PriceArchiveStructureItemDetailManager PriceArchiveStructureItemsDetailManager = new PriceArchiveStructureItemDetailManager();
            ProjectObserverSelectedManager ProjectObserverSelectedManager = new ProjectObserverSelectedManager();
            int WagePercent;
            int PreFoundation = 0;
            double Foundation = 0;
            double FoundationProject = 0;
            double FoundationCalculation = 0;
            double AssignedObserver = 0;
            Boolean HasAssignedCoordinatorObserver = false;
            decimal ObsPriceOldFundation = 0;
            decimal ObsPrice = 0;
            decimal PricePreFishes = 0;
            decimal ObsPriceFinal = 0;
            double SumDecimalTotal = 0;
            int GroupId = -1;
            int GroupIdOld = -1;
            int Step = -1;
            int StructureSkeletonId = -1;
            int StructureSkeletonIdOld = -1;
            bool IsPopulationUnder25000 = false;
            decimal ExteraFloorRatio = 1;//****************ضریب توسعه***********
            bool firstObsCevil = false;
            Boolean IsObsMother = false;

            ArrayList ArrayResult = new ArrayList();
            ArrayResult.Add(0);
            ArrayResult.Add(-1);

            #region بدست آوردن اطلاعات پروژه بر اساس درخواست جاری
            this.FindByCode(PrjReqId);
            if (this.Count <= 0)
            {
                return ArrayResult;
            }
            int ProjectStatusId = Convert.ToInt32(this[0]["ProjectStatusId"]);
            int PrjReTypeId = Convert.ToInt32(this[0]["PrjReTypeId"]);
            WagePercent = Convert.ToInt32(this[0]["WagePercent"]);
            FoundationProject = Foundation = Convert.ToDouble(this[0]["FoundationRequest"]);
            Step = Convert.ToInt32(this[0]["MaxStageNum"]);
            #region نوع اسکلت            
            if (!Utility.IsDBNullOrNullValue(this[0]["StructureSkeletonId"]))
                StructureSkeletonId = Convert.ToInt32(this[0]["StructureSkeletonId"]);

            GroupId = Convert.ToInt32(this[0]["GroupId"]);
            int DiscountPercentId = Convert.ToInt32(this[0]["DiscountPercentId"]);

            if (GroupId == (int)TSP.DataManager.TSStructureGroups.A && StructureSkeletonId == -1)
            {
                return ArrayResult;
            }
            if (GroupId != (int)TSP.DataManager.TSStructureGroups.A || StructureSkeletonId != (int)TSStructureSkeleton.Ajory)
                StructureSkeletonId = -1;

            #endregion
            #region اطلاعات درخواست قبل
            //متراژ در درخواست های قبلی که به تایید و پایان بررسی رسیده را بدست می آوریم و با متراژ فعلی مقایسه می کنیم
            DataTable dtPreRrjRest = this.SelectPreviousProjectRequestStageAndFoundation(ProjectId, PrjReqId);
            if (dtPreRrjRest.Rows.Count == 1)
            {
                GroupIdOld = Convert.ToInt32(dtPreRrjRest.Rows[0]["GroupId"]);
                if (!Utility.IsDBNullOrNullValue(dtPreRrjRest.Rows[0]["StructureSkeletonId"]))
                    StructureSkeletonIdOld = Convert.ToInt32(dtPreRrjRest.Rows[0]["StructureSkeletonId"]);
                if (!Utility.IsDBNullOrNullValue(dtPreRrjRest.Rows[0]["IsPopulationUnder25000"]))
                    IsPopulationUnder25000 = Convert.ToBoolean(dtPreRrjRest.Rows[0]["IsPopulationUnder25000"]);
                PreFoundation = Convert.ToInt32(dtPreRrjRest.Rows[0]["Foundation"]);
                if (Foundation - PreFoundation > 0 && (ProjectStatusId == (int)TSProjectStatus.Start || ProjectStatusId == (int)TSProjectStatus.End))
                    Foundation = Foundation - PreFoundation;
            }
            #endregion
            if (ProjectStatusId == (int)TSProjectStatus.Start || (ProjectStatusId == (int)TSProjectStatus.End && PrjReTypeId == (int)TSProjectRequestType.AdditionalStageRequest))//**دارای ساختمان برای ناظران جدید 1.5 برابر می شود
                ExteraFloorRatio = 1.5m;
            else
                ExteraFloorRatio = 1;
            #endregion

            #region محاسبه مبلغ اصلی بدون مابه التفاوت
            for (int i = 0; i < dtObserveringredients.Rows.Count; i++)
            {
                AssignedObserver = 0;
                HasAssignedCoordinatorObserver = false;
                if (ProjectStatusId == (int)TSProjectStatus.Start)
                {
                    AssignedObserver = ProjectObserverSelectedManager.SelectTSProjectObserverAssignedFundationForFish(dtObserveringredients.Rows[i]["MajorIdList"].ToString(), ProjectId, PrjReqId, -1);
                    if (ProjectObserverSelectedManager.SelectTSProjectObserverAssignedFundationForFish(dtObserveringredients.Rows[i]["MajorIdList"].ToString(), ProjectId, PrjReqId, 1) > 0)
                        HasAssignedCoordinatorObserver = true;
                }
                #region متراژ برای هر ناظر
                //فرمول درصد متراژ هر ناظر در هر درخواست باید با در نظر گرفتن جمع خورده ها همانند صفحه ی اختصاص ناظران پیاده سازی شود 
                //Employee_TechnicalServices_Project_ObserverSelected ===>  FunctionA2()
                //حتما صفحه ی بالا را در نظر بگیرید
                double PercentOfPrjFundation = Convert.ToDouble(dtObserveringredients.Rows[i]["FoundationPercentList"].ToString().Split(',')[0]) / 100;

                SumDecimalTotal = Math.Round(((Foundation * PercentOfPrjFundation) - Math.Floor(Foundation * PercentOfPrjFundation)) + SumDecimalTotal, 1);

                if (SumDecimalTotal != 0 && SumDecimalTotal == Math.Floor(SumDecimalTotal) && FoundationProject > 100)
                {
                    FoundationCalculation = Math.Floor(((FoundationProject <= 100 ? 100 : Foundation) * PercentOfPrjFundation)) + SumDecimalTotal;
                    SumDecimalTotal = 0;
                }
                else
                {
                    FoundationCalculation = Math.Floor((FoundationProject <= 100 ? 100 : Foundation) * PercentOfPrjFundation);
                }
                FoundationCalculation = FoundationCalculation - AssignedObserver;
                #endregion

                if (!firstObsCevil &&
                    ((int)TSP.DataManager.MainMajors.Civil == Convert.ToInt32(dtObserveringredients.Rows[i]["MajorIdList"].ToString().Split(',')[0]) || (int)TSP.DataManager.MainMajors.Architecture == Convert.ToInt32(dtObserveringredients.Rows[i]["MajorIdList"].ToString().Split(',')[0])))
                {
                    IsObsMother = true;//اولین ناظر سازه/معماری را هماهنگ کننده در نظر می گیریم
                    firstObsCevil = true;
                }

                PriceArchiveManager.FindLastPriceArchive();//آخرین تعرفه فعال را تعرفه جاری می دانیم
                if (PriceArchiveManager.Count > 0)
                {
                    int PriceArchiveId = Convert.ToInt32(PriceArchiveManager[0]["PriceArchiveId"]);
                    PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, Step, StructureSkeletonId);
                    if (PriceArchiveStructureItemsManager.Count == 0)
                    {
                        PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, StructureSkeletonId);
                        if (PriceArchiveStructureItemsManager.Count == 0)
                            PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, -1);
                    }
                }
                if (PriceArchiveStructureItemsManager.Count > 0)
                {
                    PriceArchiveStructureItemsDetailManager.FindByItemIdAndTypeIdAndMjId(Convert.ToInt32(PriceArchiveStructureItemsManager[0]["ItemId"]), (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision, Convert.ToInt32(dtObserveringredients.Rows[i]["MajorIdList"].ToString().Split(',')[0]));
                    if (PriceArchiveStructureItemsDetailManager.Count > 0)
                    {
                        #region محاسبه دستمزد ناظر هماهنگ کننده
                        decimal CoordinatorPrice = 0;
                        if (IsObsMother)
                        { //متراژ ناظر هماهنگ کننده به اندازه متراژ کل در خواست است                           
                            CoordinatorPrice = Convert.ToDecimal(((FoundationProject <= 100 ? (100) : Foundation * WagePercent / 100))) * Convert.ToDecimal(PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]);
                            IsObsMother = false;
                        }
                        #endregion
                        ObsPrice += ExteraFloorRatio * Math.Floor(Convert.ToDecimal(FoundationCalculation) * WagePercent / 100) * Convert.ToDecimal(PriceArchiveStructureItemsDetailManager[0]["Price"]);
                        ObsPrice += (1) * Math.Floor(Convert.ToDecimal(AssignedObserver) * WagePercent / 100) * Convert.ToDecimal(PriceArchiveStructureItemsDetailManager[0]["Price"]);
                        if (HasAssignedCoordinatorObserver)
                            ObsPrice += (1) * CoordinatorPrice;
                        else
                            ObsPrice += ExteraFloorRatio * CoordinatorPrice;
                        ArrayResult[1] = Convert.ToDecimal(PriceArchiveStructureItemsDetailManager[0]["ItemDetailId"]);

                    }
                }


            }
            ObsPrice = ObsPrice * ObserverFishPersent / 100;
            #endregion
            if (ProjectStatusId != (int)TSProjectStatus.End)
            {
                PricePreFishes = AccountingManager.SelectTSAccountingForProjectObserverByProject(ProjectId, GroupId == GroupIdOld ? PrjReqId : -1);
            }
            #region محاسبه  ما به تفاوت شروع ساخت و ساز
            ///********************برای حالت زمین فاقد ساختمان مابه تفاوت در محاسبه مبلغ اصلی در نظر گرفته شده است و نیازی به محاسبه مجدد در این قسمت ندارد********************//
            if (ProjectStatusId == (int)TSProjectStatus.Start && GroupId != GroupIdOld)
            {
                for (int i = 0; i < dtObserveringredients.Rows.Count; i++)
                {
                    #region متراژ برای هر ناظر
                    //فرمول درصد متراژ هر ناظر در هر درخواست باید با در نظر گرفتن جمع خورده ها همانند صفحه ی اختصاص ناظران پیاده سازی شود 
                    //Employee_TechnicalServices_Project_ObserverSelected ===>  FunctionA2()
                    //حتما صفحه ی بالا را در نظر بگیرید
                    double PercentOfPrjFundation = Convert.ToDouble(dtObserveringredients.Rows[i]["FoundationPercentList"].ToString().Split(',')[0]) / 100;

                    SumDecimalTotal = Math.Round(((PreFoundation * PercentOfPrjFundation) - Math.Floor(PreFoundation * PercentOfPrjFundation)) + SumDecimalTotal, 1);

                    if (SumDecimalTotal != 0 && SumDecimalTotal == Math.Floor(SumDecimalTotal) && PreFoundation > 100)
                    {
                        FoundationCalculation = Math.Floor(((PreFoundation <= 100 ? 100 : PreFoundation) * PercentOfPrjFundation)) + SumDecimalTotal;
                        SumDecimalTotal = 0;
                    }
                    else
                    {
                        FoundationCalculation = Math.Floor((PreFoundation <= 100 ? 100 : PreFoundation) * PercentOfPrjFundation);
                    }
                    FoundationCalculation = FoundationCalculation - AssignedObserver;
                    #endregion

                    if (!firstObsCevil &&
                        ((int)TSP.DataManager.MainMajors.Civil == Convert.ToInt32(dtObserveringredients.Rows[i]["MajorIdList"].ToString().Split(',')[0]) || (int)TSP.DataManager.MainMajors.Architecture == Convert.ToInt32(dtObserveringredients.Rows[i]["MajorIdList"].ToString().Split(',')[0])))
                    {
                        IsObsMother = true;//اولین ناظر سازه/معماری را هماهنگ کننده در نظر می گیریم
                        firstObsCevil = true;
                    }

                    PriceArchiveManager.FindLastPriceArchive();//آخرین تعرفه فعال را تعرفه جاری می دانیم
                    if (PriceArchiveManager.Count > 0)
                    {
                        //TSStructureSkeleton.Banaee
                        int PriceArchiveId = Convert.ToInt32(PriceArchiveManager[0]["PriceArchiveId"]);
                        PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, Step, StructureSkeletonId);
                        if (PriceArchiveStructureItemsManager.Count == 0)
                        {
                            PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, StructureSkeletonId);
                            if (PriceArchiveStructureItemsManager.Count == 0)
                                PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, -1);
                        }
                    }
                    if (PriceArchiveStructureItemsManager.Count > 0)
                    {
                        PriceArchiveStructureItemsDetailManager.FindByItemIdAndTypeIdAndMjId(Convert.ToInt32(PriceArchiveStructureItemsManager[0]["ItemId"]), (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision, Convert.ToInt32(dtObserveringredients.Rows[i]["MajorIdList"].ToString().Split(',')[0]));
                        if (PriceArchiveStructureItemsDetailManager.Count > 0)
                        {
                            #region محاسبه دستمزد ناظر هماهنگ کننده
                            decimal CoordinatorPrice = 0;
                            if (IsObsMother)
                            { //متراژ ناظر هماهنگ کننده به اندازه متراژ کل در خواست است                           
                                CoordinatorPrice = Convert.ToDecimal(((PreFoundation <= 100 ? (100) : PreFoundation * WagePercent / 100))) * Convert.ToDecimal(PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]);
                                IsObsMother = false;
                            }
                            #endregion
                            ObsPriceOldFundation += Math.Floor(Convert.ToDecimal(FoundationCalculation) * WagePercent / 100) * Convert.ToDecimal(PriceArchiveStructureItemsDetailManager[0]["Price"]);
                            ObsPriceOldFundation += CoordinatorPrice;
                        }
                    }
                }
                ObsPriceOldFundation = ObsPriceOldFundation * ObserverFishPersent / 100;
            }
            #endregion
            ObsPriceFinal = ObsPrice + ObsPriceOldFundation - PricePreFishes;
            ArrayResult[0] = ObsPriceFinal;
            return ArrayResult;
        }

        private DataTable GetIngredientsPercentList(int ProjectFoundation, int StructureSkeletonIdOld, int GroupIdOld, Boolean IsPopulationUnder25000)
        {
            int StructureSkeletonId = StructureSkeletonIdOld == 3 ? 3 : -1;
            string ExecptionMajorIdList = "";
            if (GroupIdOld == (int)TSP.DataManager.TSStructureGroups.A && StructureSkeletonIdOld == (int)TSP.DataManager.TSStructureSkeleton.Ajory && IsPopulationUnder25000)//***گروه ساختمانی الف اسکلت بنایی و  جمعیت شهر زیر 25000 نفر
            {
                ExecptionMajorIdList = ((int)TSP.DataManager.MainMajors.Electronic).ToString() + "," + ((int)TSP.DataManager.MainMajors.Mechanic).ToString();
            }
            else
                ExecptionMajorIdList = "";
            TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager ProjectIngridientMajorsManager = new TSP.DataManager.TechnicalServices.ProjectIngridientMajorsManager();
            DataTable dtProjectIngridien = ProjectIngridientMajorsManager.SelectTSProjectObserverMajorByProjectInfo(GroupIdOld, ProjectFoundation, StructureSkeletonId, ExecptionMajorIdList);
            return dtProjectIngridien;
        }
        //**************************
        /// <summary>
        ///***** تابع محاسبه فیش نظارت بعد از ارجاع و بر اساس متراژهای ناظران******
        ///محاسبه هزینه دستمزد ناظرین :
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="ObserverList"></param>       
        /// <returns>Price </returns>
        public ArrayList GetObserversPriceByRequest(int ProjectId, int PrjReqId, TransactionManager TransManager, int ObserverFishPersent = 100, string ObserverFilter = "")
        {

            return GetObserversPriceByRequestNew(ProjectId, PrjReqId, TransManager, ObserverFishPersent, ObserverFilter);
        }

        public ArrayList GetObserversPriceByRequestOLd(int ProjectId, int PrjReqId, TransactionManager TransManager, int ObserverFishPersent = 100, string ObserverFilter = "")
        {
            AccountingDetailManager AccountingDetailManager = new AccountingDetailManager();
            AccountingManager AccountingManager = new AccountingManager();
            PriceArchiveManager PriceArchiveManager = new PriceArchiveManager();
            PriceArchiveStructureItemsManager PriceArchiveStructureItemsManager = new PriceArchiveStructureItemsManager();
            PriceArchiveStructureItemDetailManager PriceArchiveStructureItemsDetailManager = new PriceArchiveStructureItemDetailManager();
            Project_ObserversManager ObserversManager = new Project_ObserversManager();
            ProjectCapacityDecrementManager CapacityDecrementManager = new ProjectCapacityDecrementManager();
            ProjectIngridientMajorsManager ProjectIngridientMajorsManager = new ProjectIngridientMajorsManager();
            if (TransManager != null)
            {
                TransManager.Add(ObserversManager);
                TransManager.Add(CapacityDecrementManager);
                TransManager.Add(AccountingDetailManager);
                TransManager.Add(AccountingManager);
            }
            int WagePercent;
            decimal ObsPrice = 0;
            double PercentOfPrjFundation = 1;
            ArrayList ArrayResult = new ArrayList();
            ArrayResult.Add(0);
            ArrayResult.Add(-1);
            ObserversManager.FindActivesByProjectId(ProjectId);
            ObserversManager.CurrentFilter = ObserverFilter;
            for (int i = 0; i < ObserversManager.DataTable.DefaultView.Count; i++)
            {
                CapacityDecrementManager.FindByPrjImpObsDsgnIdAndIngridientTypeId(Convert.ToInt32(ObserversManager.DataTable.DefaultView[i]["ProjectObserversId"]), (int)TSP.DataManager.TSProjectIngridientType.Observer);
                if (CapacityDecrementManager.Count > 0)
                {
                    int prjReqIdObserver = Convert.ToInt32(ObserversManager.DataTable.DefaultView[i]["PrjReId"]);
                    #region بدست آوردن اطلاعات پروژه بر اساس درخواستی که ناظر در آن ثبت شده است
                    this.DataTable.Clear();
                    this.FindByCode(prjReqIdObserver);
                    if (this.Count > 0)
                    {
                        #region   محاسبه مشخصات فیش ثبت شده برای درخواستی که ناظر در آن ثبت شده است
                        Boolean IsExteraFloorRatioMultimply = true;
                        string FishDate = Utility.GetDateOfToday();
                        int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers);
                        string AccTypeList = ((int)TSP.DataManager.TSAccountingAccType.ObserversFiche).ToString() + "," + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure).ToString() + "," + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation).ToString();
                        DataTable dt = AccountingManager.SelectExistAccountingByAccTypeList(prjReqIdObserver, TableType, -1, AccTypeList);
                        if (dt.Rows.Count > 0)
                        {
                            FishDate = dt.Rows[0]["FishDate"].ToString();

                            if (string.Compare(FishDate, "1399/06/25") < 0)
                            {
                                IsExteraFloorRatioMultimply = false;
                            }
                        }
                        #endregion
                        WagePercent = Convert.ToInt32(this[0]["WagePercent"]);
                        double Foundation = Convert.ToDouble(this[0]["FoundationRequest"]);
                        int Step = Convert.ToInt32(this[0]["MaxStageNum"]);
                        #region نوع اسکلت
                        int StructureSkeletonId = -1;
                        //*****                      
                        if (!Utility.IsDBNullOrNullValue(this[0]["StructureSkeletonId"]))
                            StructureSkeletonId = Convert.ToInt32(this[0]["StructureSkeletonId"]);
                        //*****
                        int GroupId = Convert.ToInt32(this[0]["GroupId"]);
                        int DiscountPercentId = Convert.ToInt32(this[0]["DiscountPercentId"]);

                        if (GroupId == (int)TSP.DataManager.TSStructureGroups.A && StructureSkeletonId == -1)
                        {
                            return ArrayResult;
                        }
                        if (GroupId != (int)TSP.DataManager.TSStructureGroups.A || StructureSkeletonId != (int)TSStructureSkeleton.Ajory)
                            StructureSkeletonId = -1;
                        #endregion
                        #region  اضافه اشکوب و توسعه بنا
                        Boolean _IsExtraFloor = false; int MaxStage = Step; Boolean ISExtraFoundation = false;
                        decimal ExteraFloorRatio = 1;
                        DataTable dtPreRrjRest = this.SelectPreviousProjectRequestStageAndFoundation(ProjectId, prjReqIdObserver);
                        if (dtPreRrjRest.Rows.Count == 1)
                        {
                            int PreFoundation = Convert.ToInt32(dtPreRrjRest.Rows[0]["Foundation"]);
                            int PreMaxStageNum = Convert.ToInt32(dtPreRrjRest.Rows[0]["MaxStageNum"]);
                            //****بعد از اعمال قوانین جدید اضافه اشکوب در اسفند 99 مجبور به بروزرسانی وضعیت پروژه در درخواست ها شدیم.به دلیل متفاوت بودن شرایط هر پروژه تصمیم بر آن شد که وضعیت هر درخواست براساس نوع آن
                            //***** بروز رسانی شود.بنابراین درخواست های پایان کار را پایان کار و درخواست های دیگری که تایید شده بودند را شروع به کار درنظر گرفته شد
                            //****در نتیجه برای محاسبه فیش ها با فرمول قدیم وضعیت درخواستی که ناظر ثبت شده است و یا درخواست قبل از آن را در نظر گرفتیم
                            if (Foundation - PreFoundation > 0 && (Convert.ToInt32(this[0]["tblPrjProjectStatusId"]) == (int)TSP.DataManager.TSProjectStatus.End || Convert.ToInt32(dtPreRrjRest.Rows[0]["ProjectStatusId"]) == (int)TSP.DataManager.TSProjectStatus.End))
                            //if (Foundation - PreFoundation > 0 && Convert.ToInt32(this[0]["tblPrjProjectStatusId"]) == (int)TSP.DataManager.TSProjectStatus.End)
                            {
                                _IsExtraFloor = true;
                            }
                            ISExtraFoundation = (Foundation - PreFoundation) != 0 ? true : false;
                            Foundation = (Foundation - PreFoundation) == 0 ? Foundation : (Foundation - PreFoundation);
                            if (_IsExtraFloor)
                                ExteraFloorRatio = 1.5m;
                        }
                        #endregion
                        DataTable dtPrjIngridient = ProjectIngridientMajorsManager.SelectTSProjectIngridientById((int)TSProjectIngridientType.Observer, GroupId, StructureSkeletonId, Convert.ToInt32(Foundation), Convert.ToInt32(ObserversManager.DataTable.DefaultView[i]["MjParentId"]));
                        if (dtPrjIngridient.Rows.Count == 1)
                        {
                            PercentOfPrjFundation = Convert.ToDouble(dtPrjIngridient.Rows[0]["Percent"]) / 100;
                        }
                        decimal Wage = Convert.ToDecimal(CapacityDecrementManager[0]["Wage"]);
                        if (!Utility.IsDBNullOrNullValue(ObserversManager.DataTable.DefaultView[i]["PriceArchiveId"]))
                        {
                            if (Convert.ToBoolean(ObserversManager.DataTable.DefaultView[i]["IsExteraFloor"]))
                            {
                                ExteraFloorRatio = 1.5m;
                            }
                            else
                            {
                                if (_IsExtraFloor)
                                    ExteraFloorRatio = 1.5m;
                                else
                                    ExteraFloorRatio = 1;
                            }
                            Boolean IsObsMother = Convert.ToBoolean(ObserversManager.DataTable.DefaultView[i]["IsMother"]);
                            PriceArchiveManager.FindById(Convert.ToInt32(ObserversManager.DataTable.DefaultView[i]["PriceArchiveId"]));

                            if (PriceArchiveManager.Count > 0)
                            {
                                //TSStructureSkeleton.Banaee
                                int PriceArchiveId = Convert.ToInt32(PriceArchiveManager[0]["PriceArchiveId"]);
                                PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, Step, StructureSkeletonId);
                                if (PriceArchiveStructureItemsManager.Count == 0)
                                {
                                    PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, StructureSkeletonId);
                                    if (PriceArchiveStructureItemsManager.Count == 0)
                                        PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, -1);
                                }
                            }

                            string FishDateDetail = Utility.GetDateOfToday();
                            Boolean IsObsMotherCalculateByFoundation = true;
                            AccountingDetailManager.SelectAccDetailByTableId(Convert.ToInt32(ObserversManager.DataTable.DefaultView[i]["ProjectObserversId"]), TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers));
                            if (AccountingDetailManager.Count > 0)
                            {
                                FishDateDetail = AccountingDetailManager[0]["FishDate"].ToString();
                                if (string.Compare("1398/07/17", FishDateDetail) > 0)
                                {
                                    IsObsMotherCalculateByFoundation = false;
                                }
                            }


                            if (PriceArchiveStructureItemsManager.Count > 0)
                            {
                                PriceArchiveStructureItemsDetailManager.FindByItemIdAndTypeIdAndMjId(Convert.ToInt32(PriceArchiveStructureItemsManager[0]["ItemId"]), (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision, GetMajorId(Convert.ToInt32(ObserversManager[i]["ObserversTypeId"])));
                                if (PriceArchiveStructureItemsDetailManager.Count > 0)
                                {
                                    decimal CoordinatorPrice = 0;
                                    if (IsObsMother)
                                    {
                                        if (IsObsMotherCalculateByFoundation)
                                        {
                                            #region محاسبه دستمزد ناظر هماهنگ کننده
                                            if (ISExtraFoundation || string.Compare(FishDate, "1399/08/01") < 0 || string.Compare(FishDateDetail, "1399/09/22") < 0)
                                                CoordinatorPrice = Convert.ToDecimal(Foundation) * Convert.ToDecimal(Utility.IsDBNullOrNullValue(PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]) ? 0 : PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]);
                                            else
                                            {
                                                if (string.Compare(FishDate, "1399/10/06") < 0)
                                                {
                                                    CoordinatorPrice = Convert.ToDecimal(((Foundation <= 100 ? (100 * PercentOfPrjFundation) : Foundation))) * Convert.ToDecimal(Utility.IsDBNullOrNullValue(PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]) ? 0 : PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]);
                                                }
                                                else if (string.Compare(FishDate, "1399/10/06") >= 0 && string.Compare(FishDate, "1399/11/14") < 0)
                                                {
                                                    CoordinatorPrice = Convert.ToDecimal(((Foundation <= 100 ? (100) : Foundation))) * Convert.ToDecimal(Utility.IsDBNullOrNullValue(PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]) ? 0 : PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]);
                                                }
                                                else//**********بر اساس بازخودر شماره 349 ارسالی در تاریخ1399/11/09  در پروژه هايي كه به هر دليل كسر ظرفيت مقدار واقعي پروژه نيست مثل اقدام ملي مسكن يا پروژه هاي  بافت فرسوده و ..،(ضریب کسر دستمزد برای پروژه های خاص دارند) ملاك محاسبه تمام حق الزحمه ها مقدار كسر ظرفيت است.
                                                    CoordinatorPrice = Convert.ToDecimal(((Foundation <= 100 ? (100) : Foundation * WagePercent / 100))) * Convert.ToDecimal(Utility.IsDBNullOrNullValue(PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]) ? 0 : PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]);
                                            }
                                            #endregion
                                        }
                                        else
                                            CoordinatorPrice = Convert.ToDecimal(Wage) * Convert.ToDecimal(Utility.IsDBNullOrNullValue(PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]) ? 0 : PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]);
                                    }
                                    ObsPrice += ExteraFloorRatio * Wage * Convert.ToDecimal(PriceArchiveStructureItemsDetailManager[0]["Price"]);
                                    if (IsExteraFloorRatioMultimply)
                                        ObsPrice += ExteraFloorRatio * CoordinatorPrice;
                                    else
                                        ObsPrice += CoordinatorPrice;
                                    ArrayResult[1] = Convert.ToDecimal(PriceArchiveStructureItemsDetailManager[0]["ItemDetailId"]);
                                }
                            }
                        }
                    }
                    #endregion
                }
            }


            ObsPrice = ObsPrice * ObserverFishPersent / 100;
            ArrayResult[0] = ObsPrice;
            return ArrayResult;
        }
        public ArrayList GetObserversPriceByRequestNew(int ProjectId, int PrjReqId, TransactionManager TransManager, int ObserverFishPersent = 100, string ObserverFilter = "")
        {
            AccountingDetailManager AccountingDetailManager = new AccountingDetailManager();
            AccountingManager AccountingManager = new AccountingManager();
            PriceArchiveManager PriceArchiveManager = new PriceArchiveManager();
            PriceArchiveStructureItemsManager PriceArchiveStructureItemsManager = new PriceArchiveStructureItemsManager();
            PriceArchiveStructureItemDetailManager PriceArchiveStructureItemsDetailManager = new PriceArchiveStructureItemDetailManager();
            Project_ObserversManager ObserversManager = new Project_ObserversManager();
            ProjectCapacityDecrementManager CapacityDecrementManager = new ProjectCapacityDecrementManager();
            ProjectIngridientMajorsManager ProjectIngridientMajorsManager = new ProjectIngridientMajorsManager();
            if (TransManager != null)
            {
                TransManager.Add(ObserversManager);
                TransManager.Add(CapacityDecrementManager);
                TransManager.Add(AccountingDetailManager);
                TransManager.Add(AccountingManager);
            }
            int WagePercent;
            decimal ObsPrice = 0;
            int StructureSkeletonId = -1;
            int PreFoundation = 0;
            double FoundationProject = 0;
            double Foundation = 0;
            double FoundationCoordinator = 0;
            ArrayList ArrayResult = new ArrayList();
            ArrayResult.Add(0);
            ArrayResult.Add(-1);
            ObserversManager.FindActivesByProjectId(ProjectId);
            ObserversManager.CurrentFilter = ObserverFilter;
            for (int i = 0; i < ObserversManager.DataTable.DefaultView.Count; i++)
            {
                CapacityDecrementManager.FindByPrjImpObsDsgnIdAndIngridientTypeId(Convert.ToInt32(ObserversManager.DataTable.DefaultView[i]["ProjectObserversId"]), (int)TSP.DataManager.TSProjectIngridientType.Observer);
                if (CapacityDecrementManager.Count > 0)
                {
                    int prjReqIdObserver = Convert.ToInt32(ObserversManager.DataTable.DefaultView[i]["PrjReId"]);
                    Boolean ISObserverOld = ObserversManager.CheckObserverWasInThePreRequest(prjReqIdObserver, Convert.ToInt32(ObserversManager.DataTable.DefaultView[i]["MeOfficeOthPEngOId"]), Convert.ToInt32(ObserversManager.DataTable.DefaultView[i]["MemberTypeId"]));
                    #region بدست آوردن اطلاعات پروژه بر اساس درخواستی که ناظر در آن ثبت شده است
                    this.DataTable.Clear();
                    this.FindByCode(PrjReqId);
                    if (this.Count <= 0)
                    {
                        return ArrayResult;
                    }
                    #region   محاسبه مشخصات فیش ثبت شده برای درخواستی که ناظر در آن ثبت شده است
                    int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProject_Observers);
                    string AccTypeList = ((int)TSP.DataManager.TSAccountingAccType.ObserversFiche).ToString() + "," + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Structure).ToString() + "," + ((int)TSP.DataManager.TSAccountingAccType.ObserversFicheFivePercent_Instalation).ToString();
                    //**تاریخ فیش ثبت شده در درخواستی که ناظر ثبت شده است را برای انتخاب روش محاسبه مبلغ بررسی می کنیم
                    DataTable dt = AccountingManager.SelectExistAccountingByAccTypeList(prjReqIdObserver, TableType, -1, AccTypeList);
                    if (dt.Rows.Count > 0)
                    {
                        string FishDate = dt.Rows[0]["FishDate"].ToString();
                        if (string.Compare(FishDate, "1399/12/06") < 0)
                        {    //*********************محاسبه فیش های قبل از تاریخ به شیوه قدیم***********************      
                            return GetObserversPriceByRequestOLd(ProjectId, prjReqIdObserver, TransManager, ObserverFishPersent, ObserverFilter);
                        }
                    }
                    #endregion

                    decimal ExteraFloorRatio = 1;//****************ضریب توسعه***********
                    int ProjectStatusId = Convert.ToInt32(this[0]["ProjectStatusId"]);
                    int PrjReTypeId = Convert.ToInt32(this[0]["PrjReTypeId"]);
                    WagePercent = Convert.ToInt32(this[0]["WagePercent"]);
                    FoundationCoordinator = FoundationProject = Foundation = Convert.ToDouble(this[0]["FoundationRequest"]);
                    int Step = Convert.ToInt32(this[0]["MaxStageNum"]);
                    #region نوع اسکلت                                    
                    if (!Utility.IsDBNullOrNullValue(this[0]["StructureSkeletonId"]))
                        StructureSkeletonId = Convert.ToInt32(this[0]["StructureSkeletonId"]);
                    int GroupId = Convert.ToInt32(this[0]["GroupId"]);
                    int DiscountPercentId = Convert.ToInt32(this[0]["DiscountPercentId"]);
                    if (GroupId == (int)TSP.DataManager.TSStructureGroups.A && StructureSkeletonId == -1)
                    {
                        return ArrayResult;
                    }
                    if (GroupId != (int)TSP.DataManager.TSStructureGroups.A || StructureSkeletonId != (int)TSStructureSkeleton.Ajory)
                        StructureSkeletonId = -1;
                    #endregion
                    #region  اطلاعات درخواست قبل
                    //متراژ در درخواست های قبلی ( نسبت به درخواست ثبت ناظر ) که به تایید و پایان بررسی رسیده را بدست می آوریم و با متراژ فعلی مقایسه می کنیم
                    int MaxStage = Step;// Boolean ISExtraFoundation = false;                      
                    DataTable dtPreRrjRequest = this.SelectPreviousProjectRequestStageAndFoundation(ProjectId, prjReqIdObserver);
                    if (dtPreRrjRequest.Rows.Count == 1)
                    {
                        PreFoundation = Convert.ToInt32(dtPreRrjRequest.Rows[0]["Foundation"]);
                        if (Foundation - PreFoundation != 0 && (ProjectStatusId == (int)TSProjectStatus.Start || ProjectStatusId == (int)TSProjectStatus.End || ISObserverOld))
                            FoundationCoordinator = (Foundation - PreFoundation) == 0 ? Foundation : (Foundation - PreFoundation);
                        if (Foundation - PreFoundation != 0 && (ProjectStatusId == (int)TSProjectStatus.Start || ProjectStatusId == (int)TSProjectStatus.End))
                            Foundation = (Foundation - PreFoundation) == 0 ? Foundation : (Foundation - PreFoundation);

                    }
                    #endregion
                    //****اگر ناظر در درخواست های قبل وجود داشته باشد در هر صورتی ضریب 1.5 نمی گیرد
                    //**دارای ساختمان برای ناظران جدید 1.5 برابر می شود
                    if (((ProjectStatusId == (int)TSProjectStatus.Start && PrjReTypeId != (int)TSProjectRequestType.InsertProject) || (ProjectStatusId == (int)TSProjectStatus.End && PrjReTypeId == (int)TSProjectRequestType.AdditionalStageRequest)) && !ISObserverOld)
                        ExteraFloorRatio = 1.5m;
                    else
                        ExteraFloorRatio = 1;
                    #endregion
                    decimal Wage = Convert.ToDecimal(CapacityDecrementManager[0]["Wage"]);
                    #region محاسبه تعرفه
                    int PriceArchiveId = -2;

                    if (PrjReqId != prjReqIdObserver)
                    {
                        //*********بر اساس تغییرات اسفند 99 و جدول اضافه اشکوب برای محاسبه مابه تفاوت دستمزد درصورت توسعه بنا یا اضافه اشکوب بایستی آخرین درخواست توسعه بنا/اضافه اشکوب را پیدا کرد  و گروه ساختمانی آن را در نظر بگیریم
                        DataTable dtAccFish = AccountingManager.SelectExistAccountingByAccTypeList(PrjReqId, TableType, -1, AccTypeList);
                        if (dtAccFish.Rows.Count > 0)//**پیدا کردن فیشی که در درخواست اضافه اشکوب/توسعه ثبت شده است 
                        {
                            string FishDateLastPrj = dtAccFish.Rows[0]["FishDate"].ToString();

                            PriceArchiveId = PriceArchiveManager.SelectTSPriceArchiveByDate(FishDateLastPrj);
                        }
                        else
                            PriceArchiveId = Convert.ToInt32(ObserversManager.DataTable.DefaultView[i]["PriceArchiveId"]);
                    }
                    else if (!Utility.IsDBNullOrNullValue(ObserversManager.DataTable.DefaultView[i]["PriceArchiveId"]))
                    {
                        PriceArchiveId = Convert.ToInt32(ObserversManager.DataTable.DefaultView[i]["PriceArchiveId"]);
                    }

                    #endregion
                    #region محاسبه مبلغ اصلی با درنظر گرفتن مابه التفاوت در صورت وجود توسعه بنا/اضافه اشکوب
                    if (PriceArchiveId > 0)
                    {
                        Boolean IsObsMother = Convert.ToBoolean(ObserversManager.DataTable.DefaultView[i]["IsMother"]);

                        PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, Step, StructureSkeletonId);
                        if (PriceArchiveStructureItemsManager.Count == 0)
                        {
                            PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, StructureSkeletonId);
                            if (PriceArchiveStructureItemsManager.Count == 0)
                                PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, -1);
                        }

                        if (PriceArchiveStructureItemsManager.Count > 0)
                        {
                            PriceArchiveStructureItemsDetailManager.FindByItemIdAndTypeIdAndMjId(Convert.ToInt32(PriceArchiveStructureItemsManager[0]["ItemId"]), (int)TSP.DataManager.TechnicalServices.PriceArchiveStructureItemDetailTypeManager.Types.Supervision, GetMajorId(Convert.ToInt32(ObserversManager.DataTable.DefaultView[i]["ObserversTypeId"])));
                            if (PriceArchiveStructureItemsDetailManager.Count > 0)
                            {
                                decimal CoordinatorPrice = 0;
                                if (IsObsMother)
                                {//متراژ ناظر هماهنگ کننده به اندازه متراژ کل در خواست است                           
                                    CoordinatorPrice = Convert.ToDecimal(((FoundationProject <= 100 ? (100) : FoundationCoordinator * WagePercent / 100))) * Convert.ToDecimal(Utility.IsDBNullOrNullValue(PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]) ? 0 : PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]);
                                    //???CoordinatorPrice = Convert.ToDecimal(Wage) * Convert.ToDecimal(PriceArchiveStructureItemsManager[0]["CoordinatorPrice"]);
                                }

                                ObsPrice += ExteraFloorRatio * Wage * Convert.ToDecimal(PriceArchiveStructureItemsDetailManager[0]["Price"]);
                                ObsPrice += ExteraFloorRatio * CoordinatorPrice;
                                ArrayResult[1] = Convert.ToDecimal(PriceArchiveStructureItemsDetailManager[0]["ItemDetailId"]);
                            }
                        }
                    }
                    #endregion

                }
            }

            ObsPrice = ObsPrice * ObserverFishPersent / 100;
            ArrayResult[0] = ObsPrice;
            return ArrayResult;
        }
        /// <summary>
        ///محاسبه هزینه دستمزد ناظرین :
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="ObserverList"></param>       
        /// <returns>Price </returns>
        public decimal GetObserversPriceByRequest(int ProjectId, int PrjReqId, int ObserverFishPersent = 100, string ObserverFilter = "")
        {
            ArrayList Result = GetObserversPriceByRequest(ProjectId, PrjReqId, null, ObserverFishPersent, ObserverFilter);
            return Convert.ToDecimal(Result[0]);
        }
        ////**********************************

        /// <summary>
        /// مقدار دهی سهم بیمه-سهم ناظر-سهم سازمان در جدول ناظر
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PrjReqId"></param>
        /// <param name="TransManager"></param>
        public void UpdateObserverPrice(int ProjectId, int PrjReqId, TransactionManager TransManager)
        {
            Project_ObserversManager ProjectObserversManager = new Project_ObserversManager();
            if (TransManager != null)
            {
                TransManager.Add(ProjectObserversManager);
            }
            ProjectObserversManager.FindByProjectIdAndRequestId(ProjectId, PrjReqId, 0);
            for (int i = 0; i < ProjectObserversManager.Count; i++)
            {
                UpdateObserverPriceByObserverId(Convert.ToInt32(ProjectObserversManager[i]["ProjectObserversId"]), TransManager, ProjectObserversManager);
            }
        }

        /// <summary>
        /// مقدار دهی سهم بیمه-سهم ناظر-سهم سازمان در جدول ناظر
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="PrjReqId"></param>
        /// <param name="TransManager"></param>
        public void UpdateObserverPriceByObserverId(int ProjectObserverId, TransactionManager TransManager, Project_ObserversManager ProjectObserversManager)
        {
            TechnicalServices.ProjectRequestManager ProjectRequestManager = new ProjectRequestManager();
            ProjectObserversManager.FindByProjectObserversId(ProjectObserverId);
            if (ProjectObserversManager.Count == 1)
            {
                Int64 Wage = Convert.ToInt64(ProjectObserversManager[0]["Wage"]);
                int ProjectObserversId = Convert.ToInt32(ProjectObserversManager[0]["ProjectObserversId"]);
                decimal InsuranceFactor = Convert.ToDecimal(ProjectObserversManager[0]["InsuranceFactorTody"]);
                Int64 InsurancePrice = Convert.ToInt64(ProjectObserversManager[0]["InsurancePriceTody"]);
                //*********بر اساس تغییرات اسفند 99 و جدول اضافه اشکوب برای محاسبه مابه تفاوت دستمزد درصورت توسعه بنا یا اضافه اشکوب بایستی آخرین درخواست توسعه بنا/اضافه اشکوب را پیدا کرد  و گروه ساختمانی آن را در نظر بگیریم            
                //***به این ترتیب در صورتی پروژه دچار ما به تفاوت شده باشد مبلغ بر اساس گروه ساختمانی جدید محاسبه و در نظر گرفته می شود
                DataTable dtprjReq = ProjectRequestManager.SelectRequestIdLastVersion(Convert.ToInt32(ProjectObserversManager[0]["ProjectId"]), 1, ((int)TSProjectRequestType.AdditionalStageRequest).ToString() + "," + ((int)TSProjectRequestType.IncreaseBuildingAreaRequest).ToString());
                int prjReqId = -2;
                if (dtprjReq.Rows.Count == 1)
                    prjReqId = Convert.ToInt32(dtprjReq.Rows[0]["PrjReId"]);
                else
                    prjReqId = Convert.ToInt32(ProjectObserversManager[0]["PrjReId"]);
                ArrayList ObsPrice;
                ObsPrice = GetObserversPriceByRequest(Convert.ToInt32(ProjectObserversManager[0]["ProjectId"]), prjReqId, TransManager, 100, "ProjectObserversId=" + ProjectObserversId.ToString());
                Int64 ObserverPrice = Convert.ToInt64(ObsPrice[0]);
                int PriceArchiveItemDetailId = Convert.ToInt32(ObsPrice[1]);
                double NezamShare = 0;// Convert.ToDouble(ObserverPrice) * 3 / 100;
                double NezamKardanShare = 0;// Convert.ToDouble(ObserverPrice) * 4.5 / 100;
                double InsuranceShare = Convert.ToDouble(Wage * InsurancePrice * InsuranceFactor);
                if (Convert.ToInt32(ProjectObserversManager[0]["MemberTypeId"]) == (int)TSP.DataManager.TSMemberType.OtherPerson)
                    NezamShare = Convert.ToDouble(ObserverPrice) * 1.5 / 100;
                else
                    NezamShare = Convert.ToDouble(ObserverPrice) * 3 / 100;
                if (Convert.ToInt32(ProjectObserversManager[0]["MemberTypeId"]) == (int)TSP.DataManager.TSMemberType.OtherPerson)
                    NezamKardanShare = Convert.ToDouble(ObserverPrice) * 4.5 / 100;
                else
                    NezamKardanShare = 0;
                double ObserverShare = Convert.ToDouble(ObserverPrice) - NezamShare - InsuranceShare - NezamKardanShare;
                ProjectObserversManager[0].BeginEdit();
                ProjectObserversManager[0]["NezamShare"] = NezamShare;
                ProjectObserversManager[0]["NezamKardanShare"] = NezamKardanShare;
                if (ProjectObserversManager[0]["PayFivePercent"] == null || !Convert.ToBoolean(ProjectObserversManager[0]["PayFivePercent"]))
                {
                    ProjectObserversManager[0]["ObserverShare"] = ObserverShare;
                    ProjectObserversManager[0]["InsuranceShare"] = InsuranceShare;
                }
                else
                {
                    ProjectObserversManager[0]["ObserverShare"] = 0;
                    ProjectObserversManager[0]["InsuranceShare"] = 0;
                }
                ProjectObserversManager[0]["PriceArchiveItemDetailId"] = PriceArchiveItemDetailId;
                ProjectObserversManager[0].EndEdit();
                ProjectObserversManager.Save();
                ProjectObserversManager.AcceptChanges();
                ProjectObserversManager.DataTable.AcceptChanges();
            }
        }
        #endregion

        /// <summary>
        ///محاسبه هزینه دستمزد مجریان :
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        private int Check2In1000Fiche(int ProjectId, int PrjReId)
        {
            int Per = 0;
            decimal Amount = 0;

            Project_ImplementerManager ProjectImpManager = new Project_ImplementerManager();
            ProjectImpManager.FindImpMother(ProjectId);

            if (ProjectImpManager.Count > 0)
            {
                AccountingManager AccountingManager = new AccountingManager();
                AccountingManager.FindByTableTypeIdAndAccType(Convert.ToInt32(ProjectImpManager[0]["PrjImpId"]), (int)TSP.DataManager.TableCodes.TSProject_Implementer, (int)TSP.DataManager.TSAccountingAccType._2In1000);

                if (AccountingManager.Count == 0)
                    Per = (int)ErrorRequest.No2In1000Fiche;
                else
                {
                    for (int i = 0; i < AccountingManager.Count; i++)
                    {
                        if (Convert.ToInt32(AccountingManager[i]["Status"]) != (int)TSAccountingStatus.Payment)
                            return (int)ErrorRequest.NotPay2In1000Fish;
                        Amount += Convert.ToDecimal(AccountingManager[i]["Amount"]);
                    }

                    decimal ImpPrice;
                    if (Utility.TSProject_IsBasedOnStep())
                        ImpPrice = Get2In1000PriceByStep(ProjectId, PrjReId);
                    else
                        ImpPrice = Get2In1000PriceByRequest(ProjectId, PrjReId);

                    if (Amount != ImpPrice)
                        Per = (int)ErrorRequest.AmountOf2In1000FicheNotMatch;
                }
            }
            else Per = (int)ErrorRequest.NoImplementerAgent;

            return Per;
        }

        public decimal Get2In1000PriceByRequest(int ProjectId, int PrjReId)
        {
            ContractManager ContractManager = new ContractManager();
            PriceArchiveManager PriceArchiveManager = new PriceArchiveManager();
            PriceArchiveStructureItemsManager PriceArchiveStructureItemsManager = new PriceArchiveStructureItemsManager();
            Project_ImplementerManager ProjectImplementerManager = new Project_ImplementerManager();
            ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new ProjectCapacityDecrementManager();

            decimal Price = 0;

            this.FindByCode(PrjReId);
            if (this.Count > 0)
            {
                int GroupId = Convert.ToInt32(this[0]["GroupId"]);
                double Foundation = 0;
                int Step = Convert.ToInt32(this[0]["MaxStageNum"]);
                #region نوع اسکلت
                int StructureSkeletonId = -1;

                if (!Utility.IsDBNullOrNullValue(this[0]["StructureSkeletonId"]))
                    StructureSkeletonId = Convert.ToInt32(this[0]["StructureSkeletonId"]);

                if (GroupId == (int)TSP.DataManager.TSStructureGroups.A && StructureSkeletonId == -1)
                {
                    return 0;
                }
                if (GroupId != (int)TSP.DataManager.TSStructureGroups.A)
                    StructureSkeletonId = -1;
                #endregion
                ProjectImplementerManager.FindByProjectId(ProjectId);
                for (int i = 0; i < ProjectImplementerManager.Count; i++)
                {
                    if (!Convert.ToBoolean(ProjectImplementerManager[i]["InActive"]))
                    {
                        ProjectCapacityDecrementManager.DataTable.Clear();
                        ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(ProjectId, Convert.ToInt32(ProjectImplementerManager[0]["PrjImpId"]), (int)TSP.DataManager.TSProjectIngridientType.Implementer, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                        if (ProjectCapacityDecrementManager.Count > 0)
                            Foundation += Convert.ToDouble(ProjectCapacityDecrementManager[0]["Wage"]);
                    }
                }
                Foundation = Foundation * 0.002;

                ProjectImplementerManager.FindImpMother(ProjectId);
                if (ProjectImplementerManager.Count > 0)
                {
                    int PrjImpId = Convert.ToInt32(ProjectImplementerManager[0]["PrjImpId"]);
                    string Year = ContractManager.GetContracetYear(PrjImpId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);

                    if (Year != "")
                    {
                        PriceArchiveManager.FindByYear(Year);
                        if (PriceArchiveManager.Count <= 0)
                            PriceArchiveManager.FindLastPriceArchive();
                        if (PriceArchiveManager.Count > 0)
                        {
                            int PriceArchiveId = Convert.ToInt32(PriceArchiveManager[0]["PriceArchiveId"]);
                            PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, Step, StructureSkeletonId);
                            if (PriceArchiveStructureItemsManager.Count == 0)
                                PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, StructureSkeletonId);
                            if (PriceArchiveStructureItemsManager.Count == 0)
                                PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, GroupId, -1, -1);
                            if (PriceArchiveStructureItemsManager.Count > 0)
                                Price = Convert.ToDecimal(PriceArchiveStructureItemsManager[0]["BuildCost"]) * Convert.ToDecimal(Foundation);
                        }
                    }
                }
            }
            return Price;
        }

        public decimal Get2In1000PriceByStep(int ProjectId, int PrjReId)
        {
            ContractManager ContractManager = new ContractManager();
            PriceArchiveManager PriceArchiveManager = new PriceArchiveManager();
            PriceArchiveStructureItemsManager PriceArchiveStructureItemsManager = new PriceArchiveStructureItemsManager();
            Project_ImplementerManager ProjectImplementerManager = new Project_ImplementerManager();
            ProjectCapacityDecrementManager ProjectCapacityDecrementManager = new ProjectCapacityDecrementManager();

            decimal Price = 0;

            this.FindByCode(PrjReId);
            if (this.Count > 0)
            {
                double Foundation = 0;
                int Step = Convert.ToInt32(this[0]["MaxStageNum"]);
                #region نوع اسکلت
                int StructureSkeletonId = -1;

                if (!Utility.IsDBNullOrNullValue(this[0]["StructureSkeletonId"]))
                    StructureSkeletonId = Convert.ToInt32(this[0]["StructureSkeletonId"]);

                int GroupId = Convert.ToInt32(this[0]["GroupId"]);
                if (GroupId == (int)TSP.DataManager.TSStructureGroups.A && StructureSkeletonId == -1)
                {
                    return 0;
                }
                if (GroupId != (int)TSP.DataManager.TSStructureGroups.A)
                    StructureSkeletonId = -1;
                #endregion
                ProjectImplementerManager.FindByProjectId(ProjectId);
                for (int i = 0; i < ProjectImplementerManager.Count; i++)
                {
                    if (!Convert.ToBoolean(ProjectImplementerManager[i]["InActive"]))
                    {
                        ProjectCapacityDecrementManager.DataTable.Clear();
                        ProjectCapacityDecrementManager.FindByPrjImpObsDsgnAndIngridientTypeId(ProjectId, Convert.ToInt32(ProjectImplementerManager[0]["PrjImpId"]), (int)TSP.DataManager.TSProjectIngridientType.Implementer, (int)TSP.DataManager.TSProjectIngridientType.Implementer);
                        if (ProjectCapacityDecrementManager.Count > 0)
                            Foundation += Convert.ToDouble(ProjectCapacityDecrementManager[0]["Wage"]);
                    }
                }
                Foundation = Foundation * 0.002;

                ProjectImplementerManager.FindImpMother(ProjectId);
                if (ProjectImplementerManager.Count > 0)
                {
                    int PrjImpId = Convert.ToInt32(ProjectImplementerManager[0]["PrjImpId"]);
                    string Year = ContractManager.GetContracetYear(PrjImpId, (int)TSP.DataManager.TSProjectIngridientType.Implementer);

                    if (Year != "")
                    {
                        PriceArchiveManager.FindByYear(Year);
                        if (PriceArchiveManager.Count <= 0)
                            PriceArchiveManager.FindLastPriceArchive();
                        if (PriceArchiveManager.Count > 0)
                        {
                            int PriceArchiveId = Convert.ToInt32(PriceArchiveManager[0]["PriceArchiveId"]);
                            PriceArchiveStructureItemsManager.FindByPriceArchiveIdGroupIdStep(PriceArchiveId, -1, Step, StructureSkeletonId);
                            if (PriceArchiveStructureItemsManager.Count > 0)
                                Price = Convert.ToDecimal(PriceArchiveStructureItemsManager[0]["BuildCost"]) * Convert.ToDecimal(Foundation);
                        }
                    }
                }
            }
            return Price;
        }

        #endregion

        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.TSProjectRequest);
        }

        protected override void InitAdapter()
        {
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "TSProjectRequest";
            tableMapping.ColumnMappings.Add("PrjReId", "PrjReId");
            tableMapping.ColumnMappings.Add("ProjectId", "ProjectId");
            tableMapping.ColumnMappings.Add("PrjReTypeId", "PrjReTypeId");
            tableMapping.ColumnMappings.Add("MailNo", "MailNo");
            tableMapping.ColumnMappings.Add("MailDate", "MailDate");
            tableMapping.ColumnMappings.Add("RequestDate", "RequestDate");
            tableMapping.ColumnMappings.Add("ProjectName", "ProjectName");
            tableMapping.ColumnMappings.Add("UsageId", "UsageId");
            tableMapping.ColumnMappings.Add("GroupId", "GroupId");
            tableMapping.ColumnMappings.Add("ReconstructionCode", "ReconstructionCode");
            tableMapping.ColumnMappings.Add("Foundation", "Foundation");
            tableMapping.ColumnMappings.Add("Area", "Area");
            tableMapping.ColumnMappings.Add("RecessArea", "RecessArea");
            tableMapping.ColumnMappings.Add("RemainArea", "RemainArea");
            tableMapping.ColumnMappings.Add("DocumentArea", "DocumentArea");
            tableMapping.ColumnMappings.Add("CitId", "CitId");
            tableMapping.ColumnMappings.Add("MunId", "MunId");
            tableMapping.ColumnMappings.Add("Address", "Address");
            tableMapping.ColumnMappings.Add("ComputerCode", "ComputerCode");
            tableMapping.ColumnMappings.Add("AgentId", "AgentId");
            tableMapping.ColumnMappings.Add("DiscountPercentId", "DiscountPercentId");
            tableMapping.ColumnMappings.Add("ProjectStatusId", "ProjectStatusId");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("IsConfirmed", "IsConfirmed");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("OwnershipTypeId", "OwnershipTypeId");
            tableMapping.ColumnMappings.Add("DesignerSaved", "DesignerSaved");
            tableMapping.ColumnMappings.Add("OwnerFullName", "OwnerFullName");
            tableMapping.ColumnMappings.Add("MainRegisterNo", "MainRegisterNo");
            tableMapping.ColumnMappings.Add("MainRegion", "MainRegion");
            tableMapping.ColumnMappings.Add("MainSection", "MainSection");
            tableMapping.ColumnMappings.Add("FileUrlBuildingLicence", "FileUrlBuildingLicence");
            tableMapping.ColumnMappings.Add("FileUrlTechnicalBooklet", "FileUrlTechnicalBooklet");
            tableMapping.ColumnMappings.Add("MaxStageNum", "MaxStageNum");
            tableMapping.ColumnMappings.Add("FileUrlBuildingCertificate", "FileUrlBuildingCertificate");
            tableMapping.ColumnMappings.Add("BuildingCertificateStartDate", "BuildingCertificateStartDate");
            tableMapping.ColumnMappings.Add("BuildingCertificateExpirDate", "BuildingCertificateExpirDate");
            tableMapping.ColumnMappings.Add("BuildingCertificateNum", "BuildingCertificateNum");
            tableMapping.ColumnMappings.Add("ArchiveNo", "ArchiveNo");
            tableMapping.ColumnMappings.Add("FoundationMixSkeleton", "FoundationMixSkeleton");
            tableMapping.ColumnMappings.Add("RoofTypeId", "RoofTypeId");
            tableMapping.ColumnMappings.Add("StructureSkeletonId", "StructureSkeletonId");
            tableMapping.ColumnMappings.Add("FileUrlEndProject", "FileUrlEndProject");
            tableMapping.ColumnMappings.Add("EndProjectStartDate", "EndProjectStartDate");
            tableMapping.ColumnMappings.Add("EndProjectExpirDate", "EndProjectExpirDate");
            tableMapping.ColumnMappings.Add("EndProjectNum", "EndProjectNum");
            tableMapping.ColumnMappings.Add("BuldingCheckDate", "BuldingCheckDate");
            tableMapping.ColumnMappings.Add("FileURLBuldingCheck", "FileURLBuldingCheck");
            tableMapping.ColumnMappings.Add("ObserverSaved", "ObserverSaved");
            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            SetDefaultSelectCommandValue();

            this.Adapter.DeleteCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteTSProjectRequest";
            this.Adapter.DeleteCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PrjReId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrjReId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.InsertCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertTSProjectRequest";
            this.Adapter.InsertCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrjReTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrjReTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MailNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MailNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MailDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MailDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RequestDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RequestDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsageId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsageId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GroupId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GroupId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ReconstructionCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ReconstructionCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Foundation", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Foundation", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Area", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Area", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RecessArea", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RecessArea", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RemainArea", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RemainArea", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentArea", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentArea", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CitId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CitId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MunId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MunId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Address", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Address", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ComputerCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ComputerCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DiscountPercentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DiscountPercentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectStatusId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectStatusId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsConfirmed", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsConfirmed", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OwnershipTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OwnershipTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DesignerSaved", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DesignerSaved", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OwnerFullName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OwnerFullName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MainRegisterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MainRegisterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MainRegion", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MainRegion", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MainSection", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MainSection", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlBuildingLicence", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlBuildingLicence", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlTechnicalBooklet", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlTechnicalBooklet", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MaxStageNum", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MaxStageNum", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlBuildingCertificate", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlBuildingCertificate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildingCertificateStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuildingCertificateStartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildingCertificateExpirDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuildingCertificateExpirDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildingCertificateNum", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuildingCertificateNum", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ArchiveNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ArchiveNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FoundationMixSkeleton", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FoundationMixSkeleton", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RoofTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RoofTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StructureSkeletonId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StructureSkeletonId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlEndProject", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlEndProject", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndProjectStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndProjectStartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndProjectExpirDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndProjectExpirDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndProjectNum", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndProjectNum", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuldingCheckDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuldingCheckDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileURLBuldingCheck", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileURLBuldingCheck", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObserverSaved", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObserverSaved", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand = new global::System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateTSProjectRequest";
            this.Adapter.UpdateCommand.CommandType = global::System.Data.CommandType.StoredProcedure;
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RETURN_VALUE", global::System.Data.SqlDbType.Variant, 0, global::System.Data.ParameterDirection.ReturnValue, 0, 0, null, global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrjReTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrjReTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MailNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MailNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MailDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MailDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RequestDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RequestDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UsageId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UsageId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@GroupId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "GroupId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ReconstructionCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ReconstructionCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Foundation", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Foundation", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Area", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Area", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RecessArea", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RecessArea", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RemainArea", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RemainArea", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DocumentArea", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DocumentArea", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CitId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CitId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MunId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MunId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Address", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Address", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ComputerCode", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ComputerCode", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AgentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AgentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DiscountPercentId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DiscountPercentId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ProjectStatusId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ProjectStatusId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Description", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "Description", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@IsConfirmed", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "IsConfirmed", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@InActive", global::System.Data.SqlDbType.Bit, 0, global::System.Data.ParameterDirection.Input, 0, 0, "InActive", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@UserId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "UserId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ModifiedDate", global::System.Data.SqlDbType.DateTime, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OwnershipTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OwnershipTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DesignerSaved", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DesignerSaved", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OwnerFullName", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OwnerFullName", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MainRegisterNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MainRegisterNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MainRegion", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MainRegion", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MainSection", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MainSection", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlBuildingLicence", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlBuildingLicence", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlTechnicalBooklet", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlTechnicalBooklet", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MaxStageNum", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MaxStageNum", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlBuildingCertificate", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlBuildingCertificate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildingCertificateStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuildingCertificateStartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildingCertificateExpirDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuildingCertificateExpirDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuildingCertificateNum", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuildingCertificateNum", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ArchiveNo", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ArchiveNo", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FoundationMixSkeleton", global::System.Data.SqlDbType.Float, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FoundationMixSkeleton", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@RoofTypeId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "RoofTypeId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@StructureSkeletonId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "StructureSkeletonId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileUrlEndProject", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileUrlEndProject", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndProjectStartDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndProjectStartDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndProjectExpirDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndProjectExpirDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@EndProjectNum", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "EndProjectNum", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_PrjReId", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PrjReId", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", global::System.Data.SqlDbType.Timestamp, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PrjReId", global::System.Data.SqlDbType.Int, 4, global::System.Data.ParameterDirection.Input, 0, 0, "PrjReId", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BuldingCheckDate", global::System.Data.SqlDbType.NChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BuldingCheckDate", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FileURLBuldingCheck", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FileURLBuldingCheck", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ObserverSaved", global::System.Data.SqlDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ObserverSaved", global::System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.TechnicalServices.TechnicalServicesDataSet.TSProjectRequestDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        private void SetDefaultSelectCommandValue()
        {
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSProjectRequest";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@PrjReId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@PrjReTypeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirmed", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCode", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TableType", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectStatusId", System.Data.SqlDbType.Int);
        }
        public void FindByCode(int PrjReId)
        {
            SelectTSProjectRequestById(PrjReId, -1);
        }
        public DataTable SelectProjectRequestCount(int ProjectId, int IsConfirmed)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectRequestCount", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@IsConfirmed", IsConfirmed);
            adapter.Fill(dt);

            return dt;
        }
        private void SetDefaultSelectParameter()
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectTSProjectRequest";
            this.Adapter.SelectCommand.Parameters.Add("@PrjReId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@PrjReTypeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirmed", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCode", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TableType", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@ProjectStatusId", System.Data.SqlDbType.Int);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable FindByProject(int ProjectId)
        {
            SetDefaultSelectCommandValue();
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableTypeManager.FindTtId(TableType.TSProjectRequest);
            Fill();
            return this.DataTable;
        }

        public void FindByRequestType(int ProjectId, int PrjReTypeId, int IsConfirmed)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@PrjReTypeId"].Value = PrjReTypeId;
            this.Adapter.SelectCommand.Parameters["@IsConfirmed"].Value = IsConfirmed;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableTypeManager.FindTtId(TableType.TSProjectRequest);
            Fill();
        }

        public void FindByProjectStatusId(int ProjectId, int ProjectStatusId, int IsConfirmed)
        {
            SetDefaultSelectCommandValue();
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@ProjectId"].Value = ProjectId;
            this.Adapter.SelectCommand.Parameters["@ProjectStatusId"].Value = ProjectStatusId;
            this.Adapter.SelectCommand.Parameters["@IsConfirmed"].Value = IsConfirmed;
            this.Adapter.SelectCommand.Parameters["@TableType"].Value = TableTypeManager.FindTtId(TableType.TSProjectRequest);
            Fill();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public void SelectTSProjectRequestById(int PrjReId, int ProjectId)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectTSProjectRequestById";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);
            adapter.SelectCommand.Parameters.AddWithValue("@ProjectId", ProjectId);
            adapter.SelectCommand.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TableType.TSProjectRequest));
            adapter.Fill(DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectRequest(int PrjReId, int ProjectId, int WorkFlowCode)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectTSProjectRequest";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@PrjReId", SqlDbType.Int, 4, "PrjReId").Value = PrjReId;
            adapter.SelectCommand.Parameters.Add("@ProjectId", SqlDbType.Int, 4, "ProjectId").Value = ProjectId;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4, "WorkFlowCode").Value = WorkFlowCode;
            adapter.SelectCommand.Parameters.Add("@TableType", SqlDbType.Int, 4, "TableType").Value = TableTypeManager.FindTtId(TableType.TSProjectRequest);
            adapter.Fill(dt);
            return (dt);
        }

        public void SelectRequestLastVersion(int ProjectId, int PrjReTypeId, int IsConfirmed)
        {
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectTSProjectRequestLastVersion";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@ProjectId", SqlDbType.Int, 4, "ProjectId").Value = ProjectId;
            adapter.SelectCommand.Parameters.Add("@PrjReTypeId", SqlDbType.Int, 4, "PrjReTypeId").Value = PrjReTypeId;
            adapter.SelectCommand.Parameters.Add("@IsConfirmed", SqlDbType.Int, 4, "IsConfirmed").Value = IsConfirmed;

            adapter.Fill(DataTable);
        }

        public DataTable SelectRequestIdLastVersion(int ProjectId, int IsConfirmed, string RequestTypIdList)
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectRequestIDLastVersion", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.Add("@ProjectId", SqlDbType.Int, 4, "ProjectId").Value = ProjectId;
            adapter.SelectCommand.Parameters.Add("@IsConfirmed", SqlDbType.Int, 4, "IsConfirmed").Value = IsConfirmed;
            adapter.SelectCommand.Parameters.AddWithValue("@RequestTypIdList", RequestTypIdList);

            adapter.Fill(dt);
            return (dt);
        }

        public DataTable SelectRequestIdLastVersion(int ProjectId, int IsConfirmed)
        {
            return SelectRequestIdLastVersion(ProjectId, IsConfirmed, "");
        }
        public DataTable SelectTSProjectReIdLastConf(int ProjectId)
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectTSProjectReIdLastConf", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.Add("@ProjectId", SqlDbType.Int, 4, "ProjectId").Value = ProjectId;

            adapter.Fill(dt);
            return (dt);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectRequestByProject(int ProjectId)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectTSProjectRequestByProject";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@ProjectId", SqlDbType.Int, 4, "ProjectId").Value = ProjectId;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCodePlan", SqlDbType.Int, 4, "WorkFlowCodePlan").Value = (int)TSP.DataManager.WorkFlows.TSPlansConfirming;
            adapter.SelectCommand.Parameters.Add("@TableTypeRequest", SqlDbType.Int, 4, "TableTypeRequest").Value = (int)TSP.DataManager.TableCodes.TSProjectRequest;

            adapter.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectPreviousProjectRequestStageAndFoundation(int ProjectId, int PrjReId, int PrjReTypeId)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "PreviousProjectRequestStageAndFoundation";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@ProjectId", SqlDbType.Int, 4, "ProjectId").Value = ProjectId;
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReTypeId", PrjReTypeId);


            adapter.Fill(dt);
            return (dt);
        }
        public DataTable SelectPreviousProjectRequestStageAndFoundation(int ProjectId, int PrjReId)
        {
            return SelectPreviousProjectRequestStageAndFoundation(ProjectId, PrjReId, -1);
        }
        #region جهت استفاده در پنل مشخصات پروژه در بالای صفحات ProjectUserControl
        private DataTable GetProjectInfoDT()
        {
            DataTable ProjectInfoDT = new DataTable();
            ProjectInfoDT.Columns.Add("PrjReId");
            ProjectInfoDT.Columns.Add("ProjectId");
            ProjectInfoDT.Columns.Add("ProjectName");
            ProjectInfoDT.Columns.Add("CitId");
            ProjectInfoDT.Columns.Add("CitName");
            ProjectInfoDT.Columns.Add("FileNo");
            ProjectInfoDT.Columns.Add("MunName");
            ProjectInfoDT.Columns.Add("RegisteredNo");
            ProjectInfoDT.Columns.Add("LicenseNo");
            ProjectInfoDT.Columns.Add("GroupId");
            ProjectInfoDT.Columns.Add("GroupName");
            ProjectInfoDT.Columns.Add("OwnerName");
            ProjectInfoDT.Columns.Add("DecrementPercent");
            ProjectInfoDT.Columns.Add("WagePercent");
            ProjectInfoDT.Columns.Add("Foundation");
            ProjectInfoDT.Columns.Add("StructureSkeleton");
            ProjectInfoDT.Columns.Add("MaxStageNum");
            ProjectInfoDT.Columns.Add("StructureSkeletonId");

            ProjectInfoDT.Columns.Add("DocumentArea");
            ProjectInfoDT.Columns.Add("OwnerSSN");
            ProjectInfoDT.Columns.Add("ArchiveNo");

            ProjectInfoDT.Columns.Add("MainSection");
            ProjectInfoDT.Columns.Add("MainRegion");
            ProjectInfoDT.Columns.Add("MunId");
            ProjectInfoDT.Columns.Add("DiscountPercentId");
            ProjectInfoDT.Columns.Add("AgentId");
            ProjectInfoDT.Columns.Add("OwnerMeId");
            ProjectInfoDT.Columns.Add("ProjectStatusId");
            ProjectInfoDT.Columns.Add("AgentCode");
            ProjectInfoDT.Columns.Add("AgentCodeForPaymentIdProvince");
            ProjectInfoDT.Columns.Add("DiscountPercentCode");
            ProjectInfoDT.Columns.Add("DiscountPercentTitle");
            ProjectInfoDT.Columns.Add("TaskName");
            ProjectInfoDT.Columns.Add("TaskCode");
            ProjectInfoDT.Columns.Add("OwnerMobileNo");
            ProjectInfoDT.Columns.Add("OwnerId");
            ProjectInfoDT.Columns.Add("FoundationMixSkeleton");
            ProjectInfoDT.Columns.Add("TraceCode");
            ProjectInfoDT.Columns.Add("IsPopulationUnder25000");
            ProjectInfoDT.Columns.Add("ComputerCode");
            ProjectInfoDT.Columns.Add("PrjReTypeId");
            ProjectInfoDT.Columns.Add("tblPrjProjectStatusId");
            ProjectInfoDT.Columns.Add("ProjectStatus");
            ProjectInfoDT.Columns.Add("PrjReTypeTittle");
            ProjectInfoDT.Columns.Add("PrjReqProjectStatusId");

            return ProjectInfoDT;
        }

        public DataTable GetProjectInfo(int PrjReId)
        {
            OwnerManager OwnerManager = new OwnerManager();
            DataTable ProjectInfoDT = GetProjectInfoDT();

            DataTable dt = SelectTSProjectRequestByProjectForUserControl(PrjReId);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = ProjectInfoDT.NewRow();
                dr["PrjReId"] = PrjReId;
                dr["ProjectId"] = dt.Rows[0]["ProjectId"];
                dr["ProjectName"] = dt.Rows[0]["ProjectName"];
                dr["CitId"] = dt.Rows[0]["CitId"];
                dr["CitName"] = dt.Rows[0]["CitName"];
                dr["FileNo"] = dt.Rows[0]["FileNo"];
                dr["RegisteredNo"] = dt.Rows[0]["RegisteredNo"];
                dr["LicenseNo"] = dt.Rows[0]["LicenseNo"];
                dr["GroupId"] = dt.Rows[0]["GroupId"];
                dr["GroupName"] = dt.Rows[0]["GroupName"];
                dr["TraceCode"] = dt.Rows[0]["TraceCode"];
                dr["MainSection"] = dt.Rows[0]["MainSection"];
                dr["MainRegion"] = dt.Rows[0]["MainRegion"];
                dr["MunName"] = dt.Rows[0]["MunName"];
                dr["MunId"] = dt.Rows[0]["MunId"];
                dr["AgentId"] = dt.Rows[0]["AgentId"];
                dr["AgentCode"] = dt.Rows[0]["AgentCode"];
                if (Convert.ToInt32(dt.Rows[0]["MunId"]) == 76)//***اگر شهرداری صدرا باشد برای محاسبه شناسه استان کد نمایندگی ثبت نمی شود و 74 را در نظر گرفته می شود
                    dr["AgentCodeForPaymentIdProvince"] = "74";
                else
                    dr["AgentCodeForPaymentIdProvince"] = dt.Rows[0]["AgentCode"];
                OwnerManager.FindOwnerAgent(Convert.ToInt32(dt.Rows[0]["ProjectId"]));
                if (OwnerManager.Count > 0)
                {
                    dr["OwnerName"] = OwnerManager[0]["Name"];
                    dr["OwnerSSN"] = OwnerManager[0]["SSN"];
                    dr["OwnerMeId"] = OwnerManager[0]["MeId"];
                    dr["OwnerMobileNo"] = OwnerManager[0]["MobileNo"];
                    dr["OwnerId"] = OwnerManager[0]["OwnerId"];
                }

                dr["DecrementPercent"] = dt.Rows[0]["DecrementPercent"];
                dr["WagePercent"] = dt.Rows[0]["WagePercent"];
                dr["Foundation"] = dt.Rows[0]["Foundation"];
                dr["MaxStageNum"] = dt.Rows[0]["MaxStageNum"];
                dr["DocumentArea"] = dt.Rows[0]["DocumentArea"];
                dr["ArchiveNo"] = dt.Rows[0]["ArchiveNo"];
                dr["StructureSkeletonId"] = dt.Rows[0]["StructureSkeletonId"];
                if (!Utility.IsDBNullOrNullValue(dt.Rows[0]["StructureSkeletonId"]))
                    dr["StructureSkeleton"] = GetStructureSkeletonTitle(Convert.ToInt32(dt.Rows[0]["StructureSkeletonId"]));
                else
                    dr["StructureSkeleton"] = "";

                dr["DiscountPercentId"] = dt.Rows[0]["DiscountPercentId"];
                dr["ProjectStatusId"] = dt.Rows[0]["tblPrjProjectStatusId"];
                dr["PrjReqProjectStatusId"] = dt.Rows[0]["PrjReqProjectStatusId"];
                dr["ProjectStatus"] = dt.Rows[0]["ProjectStatus"];
                dr["DiscountPercentCode"] = dt.Rows[0]["DiscountPercentCode"];
                dr["DiscountPercentTitle"] = dt.Rows[0]["DiscountPercentTitle"];
                dr["TaskName"] = dt.Rows[0]["TaskName"];
                dr["TaskCode"] = dt.Rows[0]["TaskCode"];
                dr["FoundationMixSkeleton"] = dt.Rows[0]["FoundationMixSkeleton"];
                dr["IsPopulationUnder25000"] = dt.Rows[0]["IsPopulationUnder25000"];
                dr["ComputerCode"] = dt.Rows[0]["ComputerCode"];
                dr["PrjReTypeId"] = dt.Rows[0]["PrjReTypeId"];
                dr["PrjReTypeTittle"] = dt.Rows[0]["PrjReTypeTittle"];


                ProjectInfoDT.Rows.Add(dr);
            }
            return ProjectInfoDT;
        }
        #endregion
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectTSProjectRequestByProjectForUserControl(int PrjReId)
        {
            DataTable dt = new DataTable();
            this.Adapter.SelectCommand.Parameters.Clear();
            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "SelectTSProjectRequestByProjectForUserControl";
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);
            adapter.Fill(dt);
            return (dt);
        }
        private string GetStructureSkeletonTitle(int StructureSkeletonId)
        {
            string StructureSkeletonTitle = "";
            StructureSkeletonManager structureSkeletonManager = new StructureSkeletonManager();
            structureSkeletonManager.FindByStructureSkeletonId(StructureSkeletonId);
            return StructureSkeletonTitle = structureSkeletonManager[0]["Title"].ToString();
        }
        public void DeleteProjectRequest(int ProjectId, int PrjReqId, int WorkFlowCode)
        {
            SqlCommand cmd = new SqlCommand("spDeleteTSProjectForChangeRequest", this.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Connection = this.Connection;
                if (this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();
                cmd.Transaction = this.Transaction;
                cmd.Parameters.AddWithValue("@PrjReqId", PrjReqId);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.Parameters.AddWithValue("@WorkFlowCode", WorkFlowCode);
                cmd.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest));
                cmd.ExecuteNonQuery();
                if (this.Transaction == null)
                    this.Connection.Close();
            }
            finally
            {
                cmd.Connection = null;
                cmd = null;

            }
        }
        public void DeleteProjectInsertRequest(int ProjectId, int PrjReqId, int WorkFlowCode)
        {
            SqlCommand cmd = new SqlCommand("spDeleteTSProjectInsertRequest", this.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Connection = this.Connection;
                if (this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();
                cmd.Transaction = this.Transaction;
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.Parameters.AddWithValue("@PrjReqId", PrjReqId);
                cmd.Parameters.AddWithValue("@WorkFlowCode", WorkFlowCode);
                cmd.Parameters.AddWithValue("@TableType", TableTypeManager.FindTtId(TSP.DataManager.TableType.TSProjectRequest));
                cmd.ExecuteNonQuery();
                if (this.Transaction == null)
                    this.Connection.Close();
            }
            finally
            {
                cmd.Connection = null;
                cmd = null;

            }
        }
        public void DeleteConfirmedStateForTSProject(int PrjReId, int ConfirmedWfTaskCode)
        {
            SqlCommand cmd = new SqlCommand("DeleteConfirmedStateForTSProject", this.Connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                cmd.Connection = this.Connection;
                if (this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();
                cmd.Transaction = this.Transaction;
                cmd.Parameters.AddWithValue("@PrjReqId", PrjReId);
                cmd.Parameters.AddWithValue("@ConfirmedWfTaskCode", ConfirmedWfTaskCode);
                cmd.ExecuteNonQuery();
                if (this.Transaction == null)
                    this.Connection.Close();
            }
            finally
            {
                cmd.Connection = null;
                cmd = null;

            }
        }
        public DataTable SelectTSProjectCountIngrediant(int PrjReId)
        {

            SqlDataAdapter adapter = new SqlDataAdapter("SelectTSProjectCountIngrediantByPrjReId", this.Connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.Parameters.AddWithValue("@PrjReId", PrjReId);

            DataTable dt = new System.Data.DataTable();
            adapter.Fill(dt);
            return dt;

        }
    }
}
