using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
namespace TSP.DataManager
{
    public class DocMemberFileManager : BaseObject
    {
        enum ErrorMeFile
        {
            CanNotGenerateNewSerialNo = -2
        }

        public string FindErrorMessage(int ErrorCode)
        {
            string Message = "";
            switch (ErrorCode)
            {
                case (int)ErrorMeFile.CanNotGenerateNewSerialNo:
                    Message = "امکان ایجاد شماره سریال جدید وجود ندارد";
                    break;
            }
            return Message;
        }

        #region Private Managers
        TSP.DataManager.TransactionManager TransManager;
        TSP.DataManager.RequestInActivesManager RequestInActivesManager;
        TSP.DataManager.MemberManager MemberManager;
        TSP.DataManager.MemberMarkaziLogManager MemberMarkaziLogManager;
        TSP.DataManager.DocMemberFileMajorManager DocMemberFileMajorManager;
        TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager;

        TSP.DataManager.TechnicalServices.ObserverWorkRequestChangesManager ObserverWorkRequestChangesManager;
        TSP.DataManager.TechnicalServices.ObserverWorkRequestManager ObserverWorkRequestManager;
        #endregion

        #region Constructors
        public DocMemberFileManager()
        {
        }

        public DocMemberFileManager(TSP.DataManager.TransactionManager TransactionManager)
        {
            if (TransactionManager != null)
            {
                TransManager = TransactionManager;
                RequestInActivesManager = new RequestInActivesManager();
                MemberManager = new MemberManager();
                MemberMarkaziLogManager = new MemberMarkaziLogManager();
                DocMemberFileMajorManager = new DocMemberFileMajorManager();
                DocMemberFileDetailManager = new DocMemberFileDetailManager();
                ObserverWorkRequestManager = new TechnicalServices.ObserverWorkRequestManager();
                ObserverWorkRequestChangesManager = new TechnicalServices.ObserverWorkRequestChangesManager(TransactionManager);
                TransManager.Add(RequestInActivesManager);
                TransManager.Add(MemberManager);
                TransManager.Add(MemberMarkaziLogManager);
                TransManager.Add(DocMemberFileMajorManager);
                TransManager.Add(DocMemberFileDetailManager);
                TransManager.Add(ObserverWorkRequestManager);
                TransManager.Add(ObserverWorkRequestChangesManager);

            }
        }
        #endregion

        #region Utility Methods
        private string GetDateOfToday()
        {
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            String PersianDate = PDate.GetYear(DateTime.Today) + "/" + PDate.GetMonth(DateTime.Today).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DateTime.Today).ToString().PadLeft(2, '0');
            return PersianDate;
        }

        private static Boolean IsDBNullOrNullValue(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return true;
            if (string.IsNullOrEmpty(obj.ToString()))
                return true;
            return false;
        }

        public String AddDays(string PersianDate, int Days)
        {
            PersianCalendar FC;
            DateTime DT;
            String[] str = PersianDate.Split('/');
            DT = ShamsiToMiladi(int.Parse(str[0]), int.Parse(str[1]), int.Parse(str[2]));
            FC = new PersianCalendar();

            DateTime DtAddDays = FC.AddDays(DT, Days);
            System.Globalization.PersianCalendar PDate = new System.Globalization.PersianCalendar();
            return PDate.GetYear(DtAddDays) + "/" + PDate.GetMonth(DtAddDays).ToString().PadLeft(2, '0') + "/" + PDate.GetDayOfMonth(DtAddDays).ToString().PadLeft(2, '0');
        }

        public static DateTime ShamsiToMiladi(int Year, int Month, int Day)
        {
            PersianCalendar FC = new PersianCalendar();
            return FC.ToDateTime(Year, Month, Day, 0, 0, 0, 0);
        }
        #endregion

        #region WF Methods
        public int UpdateRequestWFStateId(int MfId, Int64 StateId)
        {
            int Per = 0;
            this.FindByCode(MfId, 0);
            if (this.Count <= 0)
                return (int)ErrorRequest.RequestIsConfirmed;
            this[0].BeginEdit();
            this[0]["CurrentWFStateId"] = StateId;
            this[0].EndEdit();
            this.Save();
            this.DataTable.AcceptChanges();
            return Per;
        }

        public int UpdateMeDocIncomplateStated(int MfId, int IncomplateMembership, int IncomplateDocFile)
        {
            int Per = 0;
            this.FindByCode(MfId, 0);
            if (this.Count <= 0)
                return (int)ErrorRequest.RequestIsConfirmed;
            this[0].BeginEdit();
            if (IncomplateMembership != -1)
                this[0]["IncomplateMembership"] = IncomplateMembership;
            if (IncomplateDocFile != -1)
                this[0]["IncomplateDocFile"] = IncomplateDocFile;
            this[0].EndEdit();
            this.Save();
            this.DataTable.AcceptChanges();
            return Per;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MfId"></param>
        /// <param name="PageMod"></param>
        /// <returns></returns>
        public WFPermission CheckWFEditPermissionForMemberPortal(int MfId, string PageMod)
        {
            WFPermission WFPermission = new DataManager.WFPermission();
            int TableType = (int)TSP.DataManager.TableCodes.DocMemberFile;
            int WFCode = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
            this.FindByCode(MfId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFile);
            if (this.Count != 1)
            {
                WFPermission.BtnEdit = WFPermission.BtnInactive = WFPermission.BtnNew = WFPermission.BtnNewRequest = WFPermission.BtnSave = false;
                return WFPermission;
            }
            //////if (Convert.ToInt32(this[0]["RequesterType"]) != (int)TSP.DataManager.DocumentRequesterType.Member)
            //////{
            //////    WFPermission.BtnEdit = WFPermission.BtnInactive = WFPermission.BtnNew = WFPermission.BtnNewRequest = WFPermission.BtnSave = false;
            //////    return WFPermission;
            //////}
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, MfId, WFCode, (int)TSP.DataManager.WorkFlowStateType.SendDocToNextStep);
            if (dtWorkFlowLastState.Rows.Count <= 0)
            {
                WFPermission.BtnEdit = WFPermission.BtnInactive = WFPermission.BtnNew = WFPermission.BtnNewRequest = WFPermission.BtnSave = false;
                return WFPermission;
            }
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
            if (CurrentTaskCode != DocMeFileSaveInfoTaskCode)
            {
                WFPermission.BtnEdit = WFPermission.BtnInactive = WFPermission.BtnNew = WFPermission.BtnNewRequest = WFPermission.BtnSave = false;
                return WFPermission;
            }
            switch (PageMod)
            {
                case "New":
                    WFPermission.BtnEdit = false;
                    WFPermission.BtnInactive = false;
                    WFPermission.BtnNew = true;
                    WFPermission.BtnNewRequest = true;
                    WFPermission.BtnSave = true;
                    break;
                case "Edit":
                    WFPermission.BtnEdit = false;
                    WFPermission.BtnInactive = false;
                    WFPermission.BtnNew = true;
                    WFPermission.BtnNewRequest = true;
                    WFPermission.BtnSave = true;
                    break;
                case "View":
                    WFPermission.BtnEdit = true;
                    WFPermission.BtnInactive = true;
                    WFPermission.BtnNew = true;
                    WFPermission.BtnNewRequest = false;
                    WFPermission.BtnSave = false;
                    break;
                default:
                    WFPermission.BtnEdit = WFPermission.BtnInactive = WFPermission.BtnNew = WFPermission.BtnNewRequest = WFPermission.BtnSave = true;
                    break;
            }
            return WFPermission;
        }

        public WFPermission CheckWFEditPermissionForMemberPortalImpDocument(int MfId, string PageMod)
        {
            WFPermission WFPermission = new DataManager.WFPermission();
            int TableType = (int)TSP.DataManager.TableCodes.DocMemberFileImp;
            int WFCode = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
            this.FindByCode(MfId, (int)TSP.DataManager.DocumentTypesOfMember.DocMemberFileImp);
            if (this.Count != 1)
            {
                WFPermission.BtnEdit = WFPermission.BtnInactive = WFPermission.BtnNew = WFPermission.BtnNewRequest = WFPermission.BtnSave = false;
                return WFPermission;
            }
            if (Convert.ToInt32(this[0]["RequesterType"]) != (int)TSP.DataManager.DocumentRequesterType.Member)
            {
                WFPermission.BtnEdit = WFPermission.BtnInactive = WFPermission.BtnNew = WFPermission.BtnNewRequest = WFPermission.BtnSave = false;
                return WFPermission;
            }
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
            DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, MfId, WFCode, (int)TSP.DataManager.WorkFlowStateType.SendDocToNextStep);
            if (dtWorkFlowLastState.Rows.Count <= 0)
            {
                WFPermission.BtnEdit = WFPermission.BtnInactive = WFPermission.BtnNew = WFPermission.BtnNewRequest = WFPermission.BtnSave = false;
                return WFPermission;
            }
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.SaveImplementDocInfo;
            if (CurrentTaskCode != DocMeFileSaveInfoTaskCode)
            {
                WFPermission.BtnEdit = WFPermission.BtnInactive = WFPermission.BtnNew = WFPermission.BtnNewRequest = WFPermission.BtnSave = false;
                return WFPermission;
            }
            switch (PageMod)
            {
                case "New":
                    WFPermission.BtnEdit = false;
                    WFPermission.BtnInactive = false;
                    WFPermission.BtnNew = true;
                    WFPermission.BtnNewRequest = true;
                    WFPermission.BtnSave = true;
                    break;
                case "Edit":
                    WFPermission.BtnEdit = false;
                    WFPermission.BtnInactive = false;
                    WFPermission.BtnNew = true;
                    WFPermission.BtnNewRequest = true;
                    WFPermission.BtnSave = true;
                    break;
                case "View":
                    WFPermission.BtnEdit = true;
                    WFPermission.BtnInactive = true;
                    WFPermission.BtnNew = true;
                    WFPermission.BtnNewRequest = false;
                    WFPermission.BtnSave = false;
                    break;
                default:
                    WFPermission.BtnEdit = WFPermission.BtnInactive = WFPermission.BtnNew = WFPermission.BtnNewRequest = WFPermission.BtnSave = true;
                    break;
            }
            return WFPermission;
        }

        public int CheckPermissionDocMemberFileConfirmingSendBackTask(int MfId, int CurrentTaskCode)
        {
            int Per = 0;
            TSP.DataManager.DocMemberExamManager DocMemberExamManager = new TSP.DataManager.DocMemberExamManager();
            this.FindByCode(MfId, 0);
            if (this.Count == 1)
            {
                if (this[0]["Type"].ToString() == "0")
                {
                    DataTable dtMeExam = DocMemberExamManager.SelectByMemberFile(MfId);
                    if (dtMeExam.Rows.Count <= 0)
                    {
                        Per = (int)ErrorRequest.ExamInfoDoesNotSave;
                    }
                }
            }
            else
            {
                Per = (int)ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }

        #region MemberFileDocument
        /// <summary>
        /// Perform the next tasks of Confirming MemberDocRequest
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfMeDocConfirming(int MfId, int CurrentUserAgentId, int CurrentUserId)
        {
            int Per = 0;
            #region DoNextTaskOfConfirming

            this.FindByCode(MfId, 0);
            if (this.Count != 1)
            {
                return (int)TSP.DataManager.ErrorWFNextStep.Error;
            }


            if (Convert.ToInt32(this[0]["Type"]) != (int)DocumentOfMemberRequestType.TransferedMemberRequest &&
               (Utility.IsDBNullOrNullValue(this[0]["SerialNo"])
            || Utility.IsDBNullOrNullValue(this[0]["RegDate"])
            || Utility.IsDBNullOrNullValue(this[0]["ExpireDate"])))
            {
                return (int)TSP.DataManager.ErrorRequest.SerialNoAndExpDateNotFilled;
            }
            this[0].BeginEdit();
            if (Convert.ToInt32(this[0]["Type"]) == (int)TSP.DataManager.DocumentOfMemberRequestType.TransferedMemberRequest)
            {
                this[0]["SerialNo"] = "000";
            }
            this[0]["IsConfirm"] = 1;
            this[0]["UserId"] = CurrentUserId;
            this[0]["ModifiedDate"] = DateTime.Now;
            this[0].EndEdit();

            if (this.Save() <= 0)
            {
                return (int)TSP.DataManager.ErrorWFNextStep.Error;
            }

            int MeId = Convert.ToInt32(this[0]["MeId"]);
            MemberManager.FindByCode(MeId);
            if (MemberManager.Count != 1)
            {
                return (int)TSP.DataManager.ErrorWFNextStep.Error;
            }
            //int? MasterMjId = null;
            string ObsDate = "";
            string MappingDate = "";
            int ObsId = -2;
            int DesId = -2;
            int ImpId = -2;
            int UrbanismId = -2;
            int TrafficId = -2;
            int MappingId = -2;
            int GasId = -2;
            int MasterMfMjId = -1;

            //-----------update docmemberfilemajor---------------------
            if (!Utility.IsDBNullOrNullValue(this[0]["MasterMfMjId"]))
                MasterMfMjId = Convert.ToInt32(this[0]["MasterMfMjId"]);
            if (MasterMfMjId == -1)
            {
                return (int)TSP.DataManager.ErrorRequest.DocumentMajorIsNotDefined;
            }

            if (!DocMemberFileMajorManager.UpdateMasterInDocMemberFileMajor(MeId, MasterMfMjId))
            {
                return (int)TSP.DataManager.ErrorWFNextStep.Error;
            }

            #region FindResponsibility
            //***************Design Grade
            DataTable dtMeFileDetailDes = DocMemberFileDetailManager.FindByResponsibility(MfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Design);
            if (dtMeFileDetailDes.Rows.Count > 0)
                DesId = int.Parse(dtMeFileDetailDes.Rows[0]["GrdId"].ToString());
            //***************Implement Grade
            DataTable dtMeFileDetailImp = DocMemberFileDetailManager.FindByResponsibility(MfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Implement);
            if (dtMeFileDetailImp.Rows.Count > 0)
                ImpId = int.Parse(dtMeFileDetailImp.Rows[0]["GrdId"].ToString());
            //***************Observation Grade
            DataTable dtMeFileDetailObs = DocMemberFileDetailManager.FindByResponsibility(MfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Observation);
            if (dtMeFileDetailObs.Rows.Count > 0)
            {
                ObsId = int.Parse(dtMeFileDetailObs.Rows[0]["GrdId"].ToString());
                ObsDate = dtMeFileDetailObs.Rows[0]["Date"].ToString();
            }
            //***************Mapping Grade
            DataTable dtMeFileDetailMapping = DocMemberFileDetailManager.FindByResponsibility(MfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Mapping);
            if (dtMeFileDetailMapping.Rows.Count > 0)
                MappingId = int.Parse(dtMeFileDetailMapping.Rows[0]["GrdId"].ToString());
            //***************Traffic Grade
            DataTable dtMeFileDetailTraffic = DocMemberFileDetailManager.FindByResponsibility(MfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Traffic);
            if (dtMeFileDetailTraffic.Rows.Count > 0)
                TrafficId = int.Parse(dtMeFileDetailTraffic.Rows[0]["GrdId"].ToString());

            //***************Urbanism Grade
            DataTable dtMeFileDetailUrbanism = DocMemberFileDetailManager.FindByResponsibility(MfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Urbanism);
            if (dtMeFileDetailUrbanism.Rows.Count > 0)
                UrbanismId = int.Parse(dtMeFileDetailUrbanism.Rows[0]["GrdId"].ToString());

            //***************Gas Grade
            DataTable dtMeFileDetailGas = DocMemberFileDetailManager.FindByResponsibility(MfId, MeId, (int)TSP.DataManager.DocumentResponsibilityType.Gas);
            if (dtMeFileDetailGas.Rows.Count > 0)
                GasId = int.Parse(dtMeFileDetailGas.Rows[0]["GrdId"].ToString());


            #endregion

            MemberManager[0].BeginEdit();
            MemberManager[0]["FileNo"] = this[0]["MFNo"];
            MemberManager[0]["FileDate"] = this[0]["ExpireDate"];
            if (DesId != -2)
                MemberManager[0]["DesId"] = DesId;
            else
                MemberManager[0]["DesId"] = DBNull.Value;
            if (ImpId != -2)
                MemberManager[0]["ImpId"] = ImpId;
            else
                MemberManager[0]["ImpId"] = DBNull.Value;
            if (ObsId != -2)
                MemberManager[0]["ObsId"] = ObsId;
            else
                MemberManager[0]["ObsId"] = DBNull.Value;
            if (UrbanismId != -2)
                MemberManager[0]["UrbanismId"] = UrbanismId;
            else
                MemberManager[0]["UrbanismId"] = DBNull.Value;
            if (TrafficId != -2)
                MemberManager[0]["TrafficId"] = TrafficId;
            else
                MemberManager[0]["TrafficId"] = DBNull.Value;
            if (MappingId != -2)
                MemberManager[0]["MappingId"] = MappingId;
            else
                MemberManager[0]["MappingId"] = DBNull.Value;

            if (GasId != -2)
                MemberManager[0]["GasId"] = GasId;
            else
                MemberManager[0]["GasId"] = DBNull.Value;

            MemberManager[0]["MasterMfMjId"] = MasterMfMjId;
            MemberManager[0].EndEdit();

            if (MemberManager.Save() <= 0)
            {
                return (int)TSP.DataManager.ErrorWFNextStep.Error;
            }

            #region MemberMarkaziLog
            DataRow drMemberMarkaziLog = MemberMarkaziLogManager.NewRow();
            drMemberMarkaziLog["MeId"] = MeId;
            drMemberMarkaziLog["Flag"] = 0;
            drMemberMarkaziLog["CreateDateTime"] = DateTime.Now;
            drMemberMarkaziLog["ModifiedDate"] = DateTime.Now;
            drMemberMarkaziLog["CreateDate"] = Utility.GetDateOfToday();
            drMemberMarkaziLog["CreateTime"] = Utility.GetCurrentTime();
            drMemberMarkaziLog["Noa"] = (int)TSP.DataManager.NoaForMarkazi.update;
            MemberMarkaziLogManager.AddRow(drMemberMarkaziLog);
            if (MemberMarkaziLogManager.Save() <= 0)
            {
                return (int)TSP.DataManager.ErrorWFNextStep.Error;
            }

            #endregion


            int per = ObserverWorkRequestChangesManager.DoAutomaticObserverWorkRequestChange(MeId, "ثبت درخواست تغییرات آماده بکاری به دلیل تایید درخواست پروانه اشتغال بکار", "تایید اتوماتیک درخواست تغییرات آماده بکاری به دلیل تایید درخواست پروانه اشتغال بکار توسط سیستم"
                  , TSObserverWorkRequestChangeType.MemberDocumentChange, CurrentUserId, MfId, this[0]["ExpireDate"].ToString(), -2, TransManager);
            #endregion
            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Rejecting MemberDocRequest
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfMeDocRejecting(int MfId, int CurrentUserId)
        {
            int Per = 0;
            this.FindByCode(MfId, (int)DocumentTypesOfMember.DocMemberFile);
            if (this.Count <= 0)
            {
                return (int)TSP.DataManager.ErrorRequest.LoseRequestInfo;
            }
            this[0].BeginEdit();
            this[0]["IsConfirm"] = 2;
            this[0]["UserId"] = CurrentUserId;
            this[0]["ModifiedDate"] = DateTime.Now;
            this[0].EndEdit();
            if (this.Save() <= 0)
            {
                return (int)TSP.DataManager.ErrorWFNextStep.Error;
            }
            RequestInActivesManager.UpdateInActiveRowByRequest(MfId, TableTypeManager.FindTtId(TableType.MemberFile), 1);
            return Per;
        }

        #endregion

        #region ObservationDoc
        /// <summary>
        /// Perform the next tasks of Confirming MemberObservationDocRequest
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfMeObsDocConfirming(int MfId, int CurrentUserAgentId, int CurrentUserId)
        {
            int Per = 0;
            this.SelectObsDocSubRequest(MfId, -1);
            if (this.Count <= 0)
            {
                return (int)ErrorRequest.LoseRequestInfo;
            }
            int MeId = int.Parse(this[0]["MeId"].ToString());
            DataTable dtMeFile = this.SelectObsDocLastVersionByMeFileId(MeId, 0);
            if (dtMeFile.Rows.Count <= 0)
            {
                return (int)ErrorWFNextStep.Error;
            }
            if (Convert.ToInt32(this[0]["Type"]) != (int)DocumentOfMemberRequestType.InActive)
            {
                if (IsDBNullOrNullValue(this[0]["SerialNo"]) || IsDBNullOrNullValue(this[0]["RegDate"]) || IsDBNullOrNullValue(this[0]["ExpireDate"]))
                {
                    return (int)ErrorWFNextStep.SerialNoAndExpireDateCanNotbeNull;
                }
            }
            this[0].BeginEdit();
            this[0]["IsConfirm"] = 1;
            if (Convert.ToInt32(this[0]["Type"]) == (int)DocumentOfMemberRequestType.InActive)
                this[0]["InActive"] = 1;
            this[0]["UserId"] = CurrentUserId;
            this[0]["ModifiedDate"] = DateTime.Now;
            this[0].EndEdit();
            if (this.Save() > 0)
            {
                Per = 0;
            }
            else
            {
                Per = (int)ErrorWFNextStep.Error;
            }




            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Rejecting MemberObservationDocRequest
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfMeObsDocRejecting(int MfId, int CurrentUserId)
        {
            int Per = 0;
            this.SelectObservationDoc(-1, MfId);
            if (this.Count > 0)
            {
                int MeId = int.Parse(this[0]["MeId"].ToString());
                DataTable dtMeFile = this.SelectObsDocLastVersionByMeFileId(MeId, 0);
                if (dtMeFile.Rows.Count > 0)
                {
                    this[0].BeginEdit();
                    this[0]["IsConfirm"] = 2;
                    this[0]["UserId"] = CurrentUserId;
                    this[0]["ModifiedDate"] = DateTime.Now;
                    this[0].EndEdit();
                    if (this.Save() > 0)
                    {
                        Per = 0;
                    }
                    else
                    {
                        Per = (int)ErrorWFNextStep.Error;
                    }
                }
                else
                {
                    Per = (int)ErrorWFNextStep.Error;
                }

            }
            else
            {
                Per = (int)ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }
        #endregion

        #region ImplimentDoc
        /// <summary>
        /// Perform the next tasks of Confirming MemberImplementDocRequest
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserAgentId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfMeImpDocConfirming(int MfId, int CurrentUserAgentId, int CurrentUserId)
        {
            int Per = 0;
            this.SelectImplementDoc(-1, MfId);
            if (this.Count > 0)
            {
                int Type = Convert.ToInt32(this[0]["Type"]);
                if (Type != (int)TSP.DataManager.DocumentOfMemberRequestType.InActive)
                {
                    if (Utility.IsDBNullOrNullValue(this[0]["SerialNo"]) ||
                        Utility.IsDBNullOrNullValue(this[0]["RegDate"]) ||
                        Utility.IsDBNullOrNullValue(this[0]["ExpireDate"]))
                    {
                        Per = (int)ErrorWFNextStep.SerialNoAndExpireDateCanNotbeNull;
                    }
                }

                this[0].BeginEdit();
                this[0]["IsConfirm"] = 1;
                if (Type == (int)TSP.DataManager.DocumentOfMemberRequestType.InActive)
                    this[0]["InActive"] = 1;
                else
                    this[0]["InActive"] = 0;
                this[0]["UserId"] = CurrentUserId;
                this[0]["ModifiedDate"] = DateTime.Now;
                this[0].EndEdit();
                int cnt = this.Save();
                if (cnt > 0)
                    Per = 0;
                else
                    Per = (int)ErrorWFNextStep.Error;

            }
            else
            {
                Per = (int)ErrorRequest.LoseRequestInfo;
            }
            return Per;
        }

        /// <summary>
        /// Perform the next tasks of Rejecting MemberImplementDocRequest
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public int DoNextTaskOfMeImpDocRejecting(int MfId, int CurrentUserId)
        {
            int Per = 0;
            this.SelectImplementDoc(-1, MfId);
            if (this.Count <= 0)
            {
                return (int)ErrorRequest.LoseRequestInfo;
            }
            int MeId = int.Parse(this[0]["MeId"].ToString());
            DataTable dtMeFile = this.SelectImpDocLastVersionByMeFileId(MeId, 0);
            if (dtMeFile.Rows.Count <= 0)
            {
                return (int)ErrorWFNextStep.Error;
            }
            this[0].BeginEdit();
            this[0]["IsConfirm"] = 2;
            if (Convert.ToInt32(this[0]["Type"]) == (int)TSP.DataManager.DocumentOfMemberRequestType.New)
                this[0]["InActive"] = 1;
            this[0]["UserId"] = CurrentUserId;
            this[0]["ModifiedDate"] = DateTime.Now;
            this[0].EndEdit();
            if (this.Save() <= 0)
            {
                return (int)ErrorWFNextStep.Error;
            }
            return Per;
        }
        #endregion

        public int DoAutomaticConfirmChangeMemberFile(TSP.DataManager.WorkFlowStateManager WorkFlowStateManager,
                  TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager, int MeId, int CurrentUserId)
        {
            this.SelectLastVersion(MeId, 0, 1);
            if (this.Count == 0) return -2;

            #region Insert DocMemberFile
            DataRow MemberFileRow = this.NewRow();
            MemberFileRow["MeId"] = MeId;
            MemberFileRow["DocType"] = 0;
            MemberFileRow["IsConfirm"] = 1;
            MemberFileRow["InActive"] = 0;
            MemberFileRow["Type"] = (int)TSP.DataManager.DocumentOfMemberRequestType.InActive;//*****ابطال   
            MemberFileRow["PrId"] = this[0]["PrId"];
            MemberFileRow["SerialNo"] = this[0]["SerialNo"];
            MemberFileRow["RegDate"] = this[0]["RegDate"];
            MemberFileRow["MFSerialNo"] = this[0]["MFSerialNo"];
            MemberFileRow["MFNo"] = this[0]["MFNo"];
            MemberFileRow["ExpireDate"] = this[0]["ExpireDate"];
            MemberFileRow["IsTemporary"] = this[0]["IsTemporary"];
            MemberFileRow["Description"] = this[0]["Description"];
            MemberFileRow["CreateDate"] = Utility.GetDateOfToday();
            MemberFileRow["FollowCode"] = 0;
            MemberFileRow["UserId"] = CurrentUserId;
            MemberFileRow["ModifiedDate"] = DateTime.Now;
            this.AddRow(MemberFileRow);
            int cn = this.Save();
            this.DataTable.AcceptChanges();
            #endregion

            if (cn > 0)
            {
                #region Insert WorkFlow
                int TaskId = -1;
                int TableId = Convert.ToInt32(this[this.Count - 1]["MfId"]);
                int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmingSaveInfo;
                WorkFlowTaskManager.FindByTaskCode(TaskCode);
                TaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);
                int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
                String Description1 = "آغاز گردش کار اتوماتیک سیستم جهت ابطال پروانه شخص حقیقی به علت لغو عضویت";
                String Description2 = "تایید و پایان بررسی ابطال پروانه شخص حقیقی توسط سیستم";

                WorkFlowStateManager.ClearBeforeFill = false;
                if (WorkFlowStateManager.InsertWorkFlowState(TableType, TableId, TaskId, Description1, 0, (int)TSP.DataManager.WorkFlowStateNmcIdType.System, CurrentUserId, 1, Utility.GetDateOfToday()) > 0)
                {
                    WorkFlowStateManager.DataTable.AcceptChanges();
                    WorkFlowTaskManager.FindByTaskCode((int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfMemberAndEndProccess);
                    TaskId = Convert.ToInt32(WorkFlowTaskManager[0]["TaskId"]);
                    if (WorkFlowStateManager.InsertWorkFlowState(TableType, TableId, TaskId, Description2, 0, (int)TSP.DataManager.WorkFlowStateNmcIdType.System, CurrentUserId, 1, Utility.GetDateOfToday()) > 0)
                    {
                        WorkFlowStateManager.DataTable.AcceptChanges();
                        return 0;
                    }
                }
                #endregion
            }
            return -1;
        }
        #endregion

        #region Permission Methods
        public static Permission GetUserPermission(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.MemberFile);
        }

        public static Permission GetUserPermissionImp(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.MemberFileImp);
        }

        public static Permission GetUserPermissionObs(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.MemberFileObs);
        }
        public static Permission GetUserPermissionAccountingUnitConfirmatDocument(int UserId, UserType ut)
        {
            return BaseObject.GetUserPermission(UserId, ut, TableType.AccountingUnitConfirmatDocument);
        }
        #endregion

        #region EPayment
        public int DoNextTaskOfBankReply(int TableId, int UltId, int NmcId, int NmcIdType, int UserId, TransactionManager Trans)
        {
            TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new DataManager.WorkFlowTaskManager();
            TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new DataManager.WorkFlowStateManager();
            Trans.Add(WorkFlowStateManager);
            int TableType = TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberFile);
            int NextTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocument;
            int NextStepTaskId = -1;
            WorkFlowTaskManager.FindByTaskCode(NextTaskCode);
            if (WorkFlowTaskManager.Count != 1)
            {
                return -1;
            }
            NextStepTaskId = int.Parse(WorkFlowTaskManager[WorkFlowTaskManager.Count - 1]["TaskId"].ToString());
            string Url = "";
            string MsgContent = "";
            return WorkFlowStateManager.SendDocToNextStep(TableType, TableId, NextStepTaskId, "ارسال پرونده پروانه اشتغال به واحد مربوطه توسط عضو", NmcId, NmcIdType, UserId, MsgContent, Url);
        }
        #endregion
        protected override void InitAdapter()
        {
            System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "DocMemberFile";
            tableMapping.ColumnMappings.Add("MfId", "MfId");
            tableMapping.ColumnMappings.Add("DocType", "DocType");
            tableMapping.ColumnMappings.Add("MeId", "MeId");
            tableMapping.ColumnMappings.Add("MFSerialNo", "MFSerialNo");
            tableMapping.ColumnMappings.Add("SerialNo", "SerialNo");
            tableMapping.ColumnMappings.Add("RegDate", "RegDate");
            tableMapping.ColumnMappings.Add("ExpireDate", "ExpireDate");
            tableMapping.ColumnMappings.Add("Type", "Type");
            tableMapping.ColumnMappings.Add("AssuranceDate", "AssuranceDate");
            tableMapping.ColumnMappings.Add("PrId", "PrId");
            tableMapping.ColumnMappings.Add("PrIdOrigin", "PrIdOrigin");
            tableMapping.ColumnMappings.Add("MFNo", "MFNo");
            tableMapping.ColumnMappings.Add("MFNoOrigin", "MFNoOrigin");
            tableMapping.ColumnMappings.Add("IsConfirm", "IsConfirm");
            tableMapping.ColumnMappings.Add("InActive", "InActive");
            tableMapping.ColumnMappings.Add("Description", "Description");
            tableMapping.ColumnMappings.Add("MailNo", "MailNo");
            tableMapping.ColumnMappings.Add("MailDate", "MailDate");
            tableMapping.ColumnMappings.Add("IsTemporary", "IsTemporary");
            tableMapping.ColumnMappings.Add("FollowCode", "FollowCode");
            tableMapping.ColumnMappings.Add("UserId", "UserId");
            tableMapping.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
            tableMapping.ColumnMappings.Add("LastTimeStamp", "LastTimeStamp");
            tableMapping.ColumnMappings.Add("ActTypeId", "ActTypeId");
            tableMapping.ColumnMappings.Add("CreateDate", "CreateDate");
            tableMapping.ColumnMappings.Add("MasterMfMjId", "MasterMfMjId");
            tableMapping.ColumnMappings.Add("RequesterType", "RequesterType");
            tableMapping.ColumnMappings.Add("KardanConfirmURL", "KardanConfirmURL");
            tableMapping.ColumnMappings.Add("AccConfirmURL", "AccConfirmURL");
            tableMapping.ColumnMappings.Add("FishAccConfirmURL", "FishAccConfirmURL");
            tableMapping.ColumnMappings.Add("CurrentWFStateId", "CurrentWFStateId");
            tableMapping.ColumnMappings.Add("IncomplateMembership", "IncomplateMembership");
            tableMapping.ColumnMappings.Add("IncomplateDocFile", "IncomplateDocFile");
            tableMapping.ColumnMappings.Add("GrdId", "GrdId");
            tableMapping.ColumnMappings.Add("ImgOldDocFrontURL", "ImgOldDocFrontURL");
            tableMapping.ColumnMappings.Add("ImgOldDocBackURL", "ImgOldDocBackURL");
            tableMapping.ColumnMappings.Add("TaxOfficeLetterURL", "TaxOfficeLetterURL");
            tableMapping.ColumnMappings.Add("PeriodImageURL", "PeriodImageURL");
            tableMapping.ColumnMappings.Add("MeTitleId", "MeTitleId");
            tableMapping.ColumnMappings.Add("ImgHSEURL", "ImgHSEURL");

            this.Adapter.TableMappings.Add(tableMapping);

            this.Adapter.SelectCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.SelectCommand.Connection = this.Connection;
            this.Adapter.SelectCommand.CommandText = "spSelectDocMemberFile";
            this.Adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.SelectCommand.Parameters.Add("@MfId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TaskCode", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TaskId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@DocType", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCode", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@IsConfirm", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@Type", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@FindHasNotSerialNo", 0);

            this.Adapter.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.DeleteCommand.Connection = this.Connection;
            this.Adapter.DeleteCommand.CommandText = "dbo.spDeleteDocMemberFile";
            this.Adapter.DeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MfId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MfId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));

            this.Adapter.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.InsertCommand.Connection = this.Connection;
            this.Adapter.InsertCommand.CommandText = "dbo.spInsertDocMemberFile";
            this.Adapter.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;


            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GrdId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GrdId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccConfirmURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AccConfirmURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@KardanConfirmURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "KardanConfirmURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FishAccConfirmURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FishAccConfirmURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaxOfficeLetterURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TaxOfficeLetterURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PeriodImageURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PeriodImageURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequesterType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RequesterType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ActTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ActTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DocType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "DocType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            //this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFSerialNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MFSerialNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpireDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AssuranceDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AssuranceDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrIdOrigin", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrIdOrigin", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MFNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFNoOrigin", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MFNoOrigin", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailNo", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsTemporary", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsTemporary", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FollowCode", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FollowCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MasterMfMjId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MasterMfMjId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CurrentWFStateId", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, 0, 0, "CurrentWFStateId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IncomplateMembership", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IncomplateMembership", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IncomplateDocFile", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IncomplateDocFile", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImgOldDocFrontURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImgOldDocFrontURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImgOldDocBackURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImgOldDocBackURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeTitleId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MeTitleId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImgHSEURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImgHSEURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));



            this.Adapter.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.Adapter.UpdateCommand.Connection = this.Connection;
            this.Adapter.UpdateCommand.CommandText = "dbo.spUpdateDocMemberFile";
            this.Adapter.UpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;

            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GrdId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "GrdId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccConfirmURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AccConfirmURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@KardanConfirmURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "KardanConfirmURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FishAccConfirmURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FishAccConfirmURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TaxOfficeLetterURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "TaxOfficeLetterURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PeriodImageURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "PeriodImageURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));

            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RequesterType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "RequesterType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreateDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "CreateDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ActTypeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "ActTypeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Variant, 0, System.Data.ParameterDirection.ReturnValue, 0, 0, null, System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DocType", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "DocType", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MeId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFSerialNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MFSerialNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SerialNo", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "SerialNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "RegDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ExpireDate", System.Data.SqlDbType.Char, 0, System.Data.ParameterDirection.Input, 0, 0, "ExpireDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Type", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "Type", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AssuranceDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "AssuranceDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PrIdOrigin", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "PrIdOrigin", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFNo", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MFNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MFNoOrigin", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MFNoOrigin", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsConfirm", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsConfirm", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@InActive", System.Data.SqlDbType.Bit, 0, System.Data.ParameterDirection.Input, 0, 0, "InActive", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Description", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "Description", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailNo", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailNo", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MailDate", System.Data.SqlDbType.NChar, 0, System.Data.ParameterDirection.Input, 0, 0, "MailDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsTemporary", System.Data.SqlDbType.TinyInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IsTemporary", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FollowCode", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "FollowCode", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "UserId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ModifiedDate", System.Data.SqlDbType.DateTime, 0, System.Data.ParameterDirection.Input, 0, 0, "ModifiedDate", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MfId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MfId", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LastTimeStamp", System.Data.SqlDbType.Timestamp, 0, System.Data.ParameterDirection.Input, 0, 0, "LastTimeStamp", System.Data.DataRowVersion.Original, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MfId", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, 0, 0, "MfId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MasterMfMjId", System.Data.SqlDbType.Int, 0, System.Data.ParameterDirection.Input, 0, 0, "MasterMfMjId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CurrentWFStateId", System.Data.SqlDbType.BigInt, 0, System.Data.ParameterDirection.Input, 0, 0, "CurrentWFStateId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IncomplateMembership", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IncomplateMembership", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IncomplateDocFile", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "IncomplateDocFile", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImgOldDocFrontURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImgOldDocFrontURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImgOldDocBackURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImgOldDocBackURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MeTitleId", System.Data.SqlDbType.SmallInt, 0, System.Data.ParameterDirection.Input, 0, 0, "MeTitleId", System.Data.DataRowVersion.Current, false, null, "", "", ""));
            this.Adapter.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ImgHSEURL", System.Data.SqlDbType.NVarChar, 0, System.Data.ParameterDirection.Input, 0, 0, "ImgHSEURL", System.Data.DataRowVersion.Current, false, null, "", "", ""));
        }

        public override System.Data.DataTable DataTable
        {
            get
            {
                if ((this._dataTable == null))
                {

                    this._dataTable = new DataManager.DocumentDataSet.DocMemberFileDataTable();
                    this.DataSet.Tables.Add(this._dataTable);
                }

                return this._dataTable;
            }
        }

        public DataTable FindByFileNo(int MeId, int FileNo, byte Status, byte Type)
        {
            DataTable dt = new DataManager.DocumentDataSet.DocMemberFileDataTable();
            SqlDataAdapter ad = new SqlDataAdapter("spSelectMemberFileByFileNo", this.Connection);
            ad.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            ad.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4);
            ad.SelectCommand.Parameters.Add("@FileNo", SqlDbType.Int, 4);
            ad.SelectCommand.Parameters.Add("@Status", SqlDbType.TinyInt, 1);
            ad.SelectCommand.Parameters.Add("@Type", SqlDbType.TinyInt, 1);
            if (string.IsNullOrEmpty(MeId.ToString()))
                MeId = -1;
            ad.SelectCommand.Parameters["@MeId"].Value = FileNo;
            if (string.IsNullOrEmpty(FileNo.ToString()))
                FileNo = -1;
            ad.SelectCommand.Parameters["@FileNo"].Value = FileNo;
            if (string.IsNullOrEmpty(Status.ToString()))
                Status = 0;
            ad.SelectCommand.Parameters["@Status"].Value = Status;
            if (string.IsNullOrEmpty(Type.ToString()))
                Type = 0;
            ad.SelectCommand.Parameters["@Type"].Value = Type;
            ad.Fill(dt);
            return (dt);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectLastMemberFile(int MeId, int FileNo)
        {
            //DataTable dt = new DataManager.NezamFarsDataSet.tblMemberFileDataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectLastMemberFile", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4);
            //adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4);
            adapter.SelectCommand.Parameters.Add("@FileNo", SqlDbType.Int, 4);
            if (string.IsNullOrEmpty(MeId.ToString()))
                MeId = -1;
            adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            //if (string.IsNullOrEmpty(MfId.ToString()))
            //    MfId = -1;
            //adapter.SelectCommand.Parameters["@MfId"].Value = MfId;
            if (string.IsNullOrEmpty(FileNo.ToString()))
                FileNo = -1;
            adapter.SelectCommand.Parameters["@FileNo"].Value = FileNo;
            adapter.Fill(DataTable);
            return (DataTable);
        }

        public void FindByMeId(int MeId)
        {
            this.ResetAllParameters();
            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;

            Fill();
        }

        /// <summary>
        /// جستجوی پروانه
        /// </summary>
        /// <param name="MeId">if ImplementDoc or OservationDoc MeId= MfId</param>
        /// <param name="DocType">if MemberDoument DocType=0 if ImplementDoc DocType=1 if ObservationDoc DocType=2</param>
        public void FindDocument(int MeId, int DocType, int MfId, int IsConfirm, int Type, int FindHasNotSerialNo)
        {
            this.Adapter.SelectCommand.CommandText = "spSelectDocMemberFile";
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.Parameters.AddWithValue("@MfId", MfId);
            this.Adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TaskCode", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TaskId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@DocType", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCode", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@FindHasNotSerialNo", FindHasNotSerialNo);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@IsConfirm", IsConfirm);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@Type", Type);

            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@DocType"].Value = DocType;
            if (DocType == 0)
                this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
            if (DocType == 1)
                this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
            if (DocType == 2)
                this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming;

            Fill();
        }

        public void FindDocument(int MeId, int MfId)
        {
            FindDocument(MeId, -1, MfId, -1, -1, -1);
        }

        /// <summary>
        /// جستجوی پروانه
        /// </summary>
        /// <param name="MeId">if ImplementDoc or OservationDoc MeId= MfId</param>
        /// <param name="DocType">Use "TSP.DataManager.DocumentOfMemberRequestType" enumeration.
        /// if MemberDoument DocType=0 if ImplementDoc DocType=1 if ObservationDoc DocType=2</param>
        ///   /// <param name="FindHasNotSerialNo">0:Not Find ,1:Find</param>
        public void FindDocumentByRequestType(int MeId, int DocType, int IsConfirm, int Type, int FindHasNotSerialNo)
        {
            FindDocument(MeId, DocType, -1, IsConfirm, Type, FindHasNotSerialNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId">if ImplementDoc or OservationDoc MeId= MfId</param>
        /// <param name="DocType">if MemberDoument DocType=0 if ImplementDoc DocType=1 if ObservationDoc DocType=2</param>
        public void FindByDocumentType(int MeId, int DocType)
        {
            this.Adapter.SelectCommand.CommandText = "dbo.spSelectDocMemberFile";
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.Parameters.Add("@MfId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TaskCode", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TaskId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@DocType", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCode", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@IsConfirm", -1);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@Type", -1);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@FindHasNotSerialNo", 0);

            this.Adapter.SelectCommand.Parameters["@MeId"].Value = MeId;
            this.Adapter.SelectCommand.Parameters["@DocType"].Value = DocType;
            if (DocType == 0)
                this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
            if (DocType == 1)
                this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
            if (DocType == 2)
                this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming;

            Fill();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MfId"></param>
        /// <param name="DocType">if MemberDoument DocType=0 if ImplementDoc DocType=1 if ObservationDoc DocType=2</param>
        public void FindByCode(int MfId, int DocType)
        {
            this.Adapter.SelectCommand.CommandText = "spSelectDocMemberFile";
            this.Adapter.SelectCommand.Parameters.Clear();
            this.Adapter.SelectCommand.Parameters.Add("@MfId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@MeId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TaskCode", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@TaskId", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@DocType", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.Add("@WorkFlowCode", System.Data.SqlDbType.Int);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@IsConfirm", -1);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@Type", -1);
            this.Adapter.SelectCommand.Parameters.AddWithValue("@FindHasNotSerialNo", 0);

            this.Adapter.SelectCommand.Parameters["@DocType"].Value = DocType;
            this.Adapter.SelectCommand.Parameters["@MfId"].Value = MfId;
            if (DocType == 0)
                this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)TSP.DataManager.WorkFlows.DocumentOfMemberConfirming;
            if (DocType == 1)
                this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)TSP.DataManager.WorkFlows.ImplementDocumentConfirming;
            if (DocType == 2)
                this.Adapter.SelectCommand.Parameters["@WorkFlowCode"].Value = (int)TSP.DataManager.WorkFlows.ObservationDocumentConfirming;

            Fill();
        }

        public int GenerateNewMemberFileSerialNo(int MeId, string MasterMjCode)
        {
            DataTable dtSerialNo = new System.Data.DataTable();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("CreateMemberDocSerialNo", this.Connection);
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand.Transaction = this.Transaction;
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@MasterMjId", -1);
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@MasterMjCode", MasterMjCode);
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@CurrentPrId", Utility.GetCurrentProvinceId());


            sqlAdapter.Fill(dtSerialNo);
            if (dtSerialNo.Rows.Count == 1)
            {
                return Convert.ToInt32(dtSerialNo.Rows[0]["NewMFSerialNo"]);
            }

            return (int)ErrorMeFile.CanNotGenerateNewSerialNo;
        }

        public Boolean CheckIfMfNoRepitetive(string MfNo, int MeId)
        {
            DataTable dtDocMe = new System.Data.DataTable();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("CheckIfMfNoExist", this.Connection);
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand.Transaction = this.Transaction;
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@MfNo", MfNo);
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);

            sqlAdapter.Fill(dtDocMe);
            if (dtDocMe.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        public DataTable SelectForReportMemberFile(int MeId, int TaskCode, int MfId)
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("spReportMemberFile", this.Connection);
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand.Transaction = this.Transaction;
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@TaskCode", TaskCode);
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@MfId", MfId);
            sqlAdapter.Fill(dt);
            return dt;
        }

        public DataTable SelectForReportMemberFile(int MeId, int TaskCode)
        {
            return SelectForReportMemberFile(MeId, TaskCode, -1);
        }

        public DataTable SelectDocMemberFileRequestFromMember()
        {
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("spSelectDocMemberFileMemberRequestCount", this.Connection);
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand.Transaction = this.Transaction;
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@TaskCodeEmployeeCheck", (int)WorkFlowTask.DocumentUnitEmployeeConfirmingDocument);
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCode", (int)WorkFlows.DocumentOfMemberConfirming);
            sqlAdapter.Fill(dt);
            return dt;
        }

        public DataTable SelectDocMemberFileBySerialNo(int SerialNo)
        {
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("spSelectDocMemberFileBySerialNo", this.Connection);
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand.Transaction = this.Transaction;
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@SerialNo", SerialNo);
            DataTable dt = new System.Data.DataTable();
            sqlAdapter.Fill(dt);
            return dt;
        }


        public DataTable SelectImpDocForWebServiceEsys(int MeId)
        {
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("SelectImpDocForWebServiceEsys", this.Connection);
            sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlAdapter.SelectCommand.Transaction = this.Transaction;
            sqlAdapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            DataTable dt = new System.Data.DataTable();
            sqlAdapter.Fill(dt);
            return dt;
        }

        #region MemberFileDoc Methods


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectDocumentMemberFile(int MfId, int MeId, int TaskCode, int TaskId, int DocType, string FollowCode)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocMemberFile", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4).Value = MfId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4).Value = TaskCode;
            adapter.SelectCommand.Parameters.Add("@TaskId", SqlDbType.Int, 4).Value = TaskId;
            adapter.SelectCommand.Parameters.Add("@DocType", SqlDbType.Int, 4).Value = DocType;
            adapter.SelectCommand.Parameters.AddWithValue("@IsConfirm", -1);
            adapter.SelectCommand.Parameters.AddWithValue("@Type", -1);
            adapter.SelectCommand.Parameters.AddWithValue("@FindHasNotSerialNo", 0);
            adapter.SelectCommand.Parameters.AddWithValue("@FollowCode", FollowCode);

            if (DocType == 0)
                adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.DocumentOfMemberConfirming;
            else if (DocType == 1)
                adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.ImplementDocumentConfirming;
            else if (DocType == 2)
                adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.ObservationDocumentConfirming;

            adapter.Fill(DataTable);
            return (DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectDocumentMemberFile(int MfId, int MeId, int TaskCode, int TaskId, int DocType)
        {
            return SelectDocumentMemberFile(MfId, MeId, TaskCode, TaskId, DocType, "%");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectDocumentMemberFile(string FollowCode, int DocType)
        {
            return SelectDocumentMemberFile(-1, -1, -1, -1, DocType, FollowCode);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByMember(int MeId, int DocType)
        {
            return (SelectDocumentMemberFile(-1, MeId, -1, -1, DocType));
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectByTaskCode(int TaskCode, int DocType)
        {
            return (SelectDocumentMemberFile(-1, -1, TaskCode, -1, -1));
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectLastVersion(int MeId, int DocType)
        {

            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileLastVersion";
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@DocType", SqlDbType.Int, 4).Value = DocType;
            adapter.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.Int, 4).Value = 1;

            DataTable.Clear();
            adapter.Fill(DataTable);
            return (DataTable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="DocType"></param>
        /// <param name="IsConfirm">0:UnNoun,1:Confime:2NotConfirme</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectLastVersion(int MeId, int DocType, int IsConfirm)
        {

            SqlDataAdapter adapter = this.Adapter;
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileLastVersion";
            adapter.SelectCommand.Parameters.Clear();
            //new SqlDataAdapter("spSelectDocMemberFileLastVersion", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@DocType", SqlDbType.Int, 4).Value = DocType;
            adapter.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.Int, 4).Value = IsConfirm;
            adapter.SelectCommand.Transaction = this.Transaction;

            DataTable.Clear();
            adapter.Fill(DataTable);
            return (DataTable);
        }

        #region spSelectDocMemberFileMainRequestForSettelment
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMainRequestForSettelment(int MeId, String FollowCode, String EndDateFrom, String EndDateTo, String FirstName, String LastName, String MFNo
            , int LastConfirmReqType, string MFNoWithOutSerial, int MFSerialNo, int TaskCodeAccConf, int TaskCode
            , string WFDate,string WFDateTo, string WFDoerName
           )
        {
            if (MeId == -1 && FollowCode == "%" && EndDateFrom == "1" && EndDateTo == "2"
                && FirstName == "%" && LastName == "%" && MFNo == "%"
           && LastConfirmReqType == -1 && MFNoWithOutSerial == "%" && MFSerialNo == -1
           && TaskCodeAccConf == -1
           && WFDate == "1" && WFDateTo == "2"&& WFDoerName == "%")
                return new DataTable();

            DataTable.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocMemberFileMainRequestForSettelment", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4);
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4).Value = TaskCode;
            adapter.SelectCommand.Parameters.AddWithValue("@DocType", 0);
            adapter.SelectCommand.Parameters.AddWithValue("@FollowCode", FollowCode);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDateFrom", EndDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDateTo", EndDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@FirstName", FirstName);
            adapter.SelectCommand.Parameters.AddWithValue("@LastName", LastName);
            adapter.SelectCommand.Parameters.AddWithValue("@LastConfirmReqType", LastConfirmReqType);
            adapter.SelectCommand.Parameters.AddWithValue("@MFNo", MFNo);
            adapter.SelectCommand.Parameters.AddWithValue("@MFNoWithOutSerial", MFNoWithOutSerial);
            adapter.SelectCommand.Parameters.AddWithValue("@MFSerialNo", MFSerialNo);
            adapter.SelectCommand.Parameters.AddWithValue("@TaskCodeAccConf", TaskCodeAccConf);
            adapter.SelectCommand.Parameters.AddWithValue("@WFDate", WFDate);
            adapter.SelectCommand.Parameters.AddWithValue("@WFDateTo", WFDateTo);            
            adapter.SelectCommand.Parameters.AddWithValue("@WFDoerName", WFDoerName);
            adapter.SelectCommand.Parameters.AddWithValue("@TaskId", -1);
            adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCode", (int)WorkFlows.DocumentOfMemberConfirming);

            adapter.Fill(DataTable);
            return (DataTable);
        }


        #endregion

        #region SelectMainRequest

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMainRequest(int MeId, int DocType, String FollowCode, String EndDateFrom, String EndDateTo, String FirstName, String LastName, String MFNo, int LastConfirmReqType, string MFNoWithOutSerial, int MFSerialNo, int TaskCodeAccConf, int TaskCode, Int16 MeDataComplete, Int16 MeDocComplete, string WFDate, string WFDoerName, Int16 PaymentType, Int16 PaymentStatus, string CreateDateLastRequst, Int16 TaskId, Boolean IsDataReturn, string WFDateTo
            , string LastRequestCreateDateFrom, string LastRequestCreateDateTo, int MjParentId, int GradeId, int LastRequsetType, int RequesterType)
        {
            if (!IsDataReturn)
                return new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocMemberFileMainRequest", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4);
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4).Value = TaskCode;
            adapter.SelectCommand.Parameters.Add("@DocType", SqlDbType.Int, 4).Value = DocType;
            adapter.SelectCommand.Parameters.AddWithValue("@FollowCode", FollowCode);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDateFrom", EndDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDateTo", EndDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@FirstName", FirstName);
            adapter.SelectCommand.Parameters.AddWithValue("@LastName", LastName);
            adapter.SelectCommand.Parameters.AddWithValue("@LastConfirmReqType", LastConfirmReqType);
            adapter.SelectCommand.Parameters.AddWithValue("@LastRequsetType", LastRequsetType);
            adapter.SelectCommand.Parameters.AddWithValue("@MFNo", MFNo);
            adapter.SelectCommand.Parameters.AddWithValue("@MFNoWithOutSerial", MFNoWithOutSerial);
            adapter.SelectCommand.Parameters.AddWithValue("@MFSerialNo", MFSerialNo);
            adapter.SelectCommand.Parameters.AddWithValue("@TaskCodeAccConf", TaskCodeAccConf);

            adapter.SelectCommand.Parameters.AddWithValue("@MeDataComplete", MeDataComplete);
            adapter.SelectCommand.Parameters.AddWithValue("@MeDocComplete", MeDocComplete);
            adapter.SelectCommand.Parameters.AddWithValue("@WFDate", WFDate);
            adapter.SelectCommand.Parameters.AddWithValue("@WFDateTo", WFDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@WFDoerName", WFDoerName);
            adapter.SelectCommand.Parameters.AddWithValue("@PaymentType", PaymentType);
            adapter.SelectCommand.Parameters.AddWithValue("@PaymentStatus", PaymentStatus);
            adapter.SelectCommand.Parameters.AddWithValue("@CreateDateLastRequst", CreateDateLastRequst);
            adapter.SelectCommand.Parameters.AddWithValue("@TaskId", TaskId);
            adapter.SelectCommand.Parameters.AddWithValue("@LastRequestCreateDateFrom", LastRequestCreateDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@LastRequestCreateDateTo", LastRequestCreateDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@MjParentId", MjParentId);
            adapter.SelectCommand.Parameters.AddWithValue("@GradeId", GradeId);
            adapter.SelectCommand.Parameters.AddWithValue("@RequesterType", RequesterType);



            if (DocType == 0)
                adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCode", (int)WorkFlows.DocumentOfMemberConfirming);
            else if (DocType == 1)
                adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCode", (int)WorkFlows.ImplementDocumentConfirming);
            else if (DocType == 2)
                adapter.SelectCommand.Parameters.AddWithValue("@WorkFlowCode", (int)WorkFlows.ObservationDocumentConfirming);

            adapter.Fill(DataTable);
            return (DataTable);
        }

        //Used In ManagmentPage
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMainRequest(int MeId, int DocType, String FollowCode, String EndDateFrom, String EndDateTo, String FirstName, String LastName, String MFNo, int LastConfirmReqType, Int16 MeDataComplete, Int16 MeDocComplete, string WFDate, string WFDateTo, string WFDoerName, Int16 PaymentType, Int16 PaymentStatus, string CreateDateLastRequst, Int16 TaskId
             , string LastRequestCreateDateFrom, string LastRequestCreateDateTo, int MjParentId, int GradeId, int LastRequsetType, int RequesterType)
        {
            Boolean IsDataReturn = true;
            if (MeId == -1 && DocType == 0 && FollowCode == "%" && EndDateFrom == "1" && EndDateTo == "2" && FirstName == "%" && LastName == "%" && MFNo == "%" && LastConfirmReqType == -1 && MeDataComplete == -1 && MeDocComplete == -1
                && WFDate == "1" && WFDateTo == "2" && WFDoerName == "%" && PaymentType == -1 && PaymentStatus == -1 && CreateDateLastRequst == "1" && TaskId == -1
                && LastRequestCreateDateFrom == "1" && LastRequestCreateDateTo == "2" && MjParentId == -1 && GradeId == -1 && RequesterType == -1)
                IsDataReturn = false;
            return SelectMainRequest(MeId, DocType, FollowCode, EndDateFrom, EndDateTo, FirstName, LastName, MFNo, LastConfirmReqType, "%", -1, -1, -1, MeDataComplete, MeDocComplete, WFDate, WFDoerName, PaymentType, PaymentStatus, CreateDateLastRequst, TaskId, IsDataReturn, WFDateTo, LastRequestCreateDateFrom, LastRequestCreateDateTo, MjParentId, GradeId, LastRequsetType, RequesterType);

        }


        #endregion

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectSubRequest(int MeId, int TaskCode, int DocType)
        {

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocMemberFileSubRequest", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4).Value = TaskCode;
            adapter.SelectCommand.Parameters.Add("@DocType", SqlDbType.Int, 4).Value = DocType;
            if (DocType == 0)
                adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.DocumentOfMemberConfirming;
            else if (DocType == 1)
                adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.ImplementDocumentConfirming;
            else if (DocType == 2)
                adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.ObservationDocumentConfirming;

            adapter.Fill(DataTable);
            return (DataTable);
        }
        public DataTable FindDocMemberFileByDate(int MeId, string FromDate, string EndDate)
        {
            DataTable.Clear();
            // ArrayList ArrDocMember = new ArrayList();
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocMemberFileByDate", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.NChar, 10).Value = FromDate;
            adapter.SelectCommand.Parameters.Add("@EndDate", SqlDbType.NChar, 10).Value = EndDate;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.DocumentOfMemberConfirming;

            adapter.Fill(DataTable);
            return DataTable;
        }

        /// <summary>
        /// کلیه پروانه هایی که در یک بازه زمانی فعال بوده اند را بر می گرداند
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="FromDate"></param>
        /// <param name="EndDate"></param>
        /// <returns>((DataRow)ArrayList[i])["MfId"] , ((DataRow)ArrayList[i])["MFNO"]: شماره پروانه,((DataRow)ArrayList[i])["Date"]:تاریخ تایید , ((DataRow)ArrayList[i])["ExpireDate"] :تاریخ پایان اعتبار پروانه</returns>
        public ArrayList FindActiveDocMemberFileByDate(int MeId, string FromDate, string EndDate)
        {
            ArrayList ArrDocMember = new ArrayList();
            DataTable dtDocMeFileIn = new DataTable();
            DataTable dtDocMeFileOut = new DataTable();
            dtDocMeFileIn = FindDocMemberFileByDate(MeId, FromDate, EndDate).Copy();
            if (dtDocMeFileIn.Rows.Count > 0)
            {
                string FirstDocDate = dtDocMeFileIn.Rows[0]["Date"].ToString();
                FirstDocDate = AddDays(FirstDocDate, -1);
                if (string.Compare(FromDate, FirstDocDate) < 0)
                // if (FirstDocDate > FromDate)
                {
                    dtDocMeFileOut = FindDocMemberFileByDate(MeId, "0000/00/00", FirstDocDate);
                    if (dtDocMeFileOut.Rows.Count > 0)
                    {
                        ArrDocMember.Add(dtDocMeFileOut.Rows[dtDocMeFileOut.Rows.Count - 1]);
                    }
                }
            }
            else
            {
                dtDocMeFileOut = FindDocMemberFileByDate(MeId, "0000/00/00", FromDate);
                if (dtDocMeFileOut.Rows.Count > 0)
                {
                    ArrDocMember.Add(dtDocMeFileOut.Rows[dtDocMeFileOut.Rows.Count - 1]);
                }
            }
            for (int i = 0; i < dtDocMeFileIn.Rows.Count; i++)
            {
                ArrDocMember.Add(dtDocMeFileIn.Rows[i]);
            }

            return ArrDocMember;
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectDocumentMemberFileByMember(int MeId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocMemberFileForMember", this.Connection);
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4).Value = -1;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4).Value = -1;
            adapter.SelectCommand.Parameters.Add("@TaskId", SqlDbType.Int, 4).Value = -1;
            adapter.SelectCommand.Parameters.Add("@DocType", SqlDbType.Int, 4).Value = (int)DocumentTypesOfMember.DocMemberFile;
            adapter.SelectCommand.Parameters.AddWithValue("@IsConfirm", -1);
            adapter.SelectCommand.Parameters.AddWithValue("@Type", -1);
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.DocumentOfMemberConfirming;

            adapter.Fill(DataTable);
            return (DataTable);
        }


        #region CheckConditions
        /// <summary>
        /// چک کردن شرایط اولیه ثبت درخواست صدور پروانه اشتغال شخص حقیقی
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUser_LoginType"></param>
        /// <returns></returns>
        public static ArrayList CheckConditionForNewDocument(int MeId, int CurrentUser_LoginType)
        {


            Boolean HasImage = false, HasIdNo = false, HasSSN = false, HasSoldire = false, HasSSNValue = false, HasKardan = false;
            ArrayList ReturnValue = new ArrayList();
            ReturnValue.Add(true);
            ReturnValue.Add("");
            ArrayList MeReqResult = Utility.CheckMemberRequestVisibility();
            if (Convert.ToBoolean(MeReqResult[0]))
            {
                ReturnValue[0] = false;
                ReturnValue[1] = MeReqResult[1];
                return ReturnValue;
            }
            string Debt = Utility.CheckMemberOfflineDebt(MeId);
            if (Debt == "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطا در ارتباط با وب سرویس دریافت میزان بدهی ایجاد شده است.";
                return ReturnValue;
            }
            if (Debt != "0" && Debt != "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "شما دارای بدهی عضویت به مبلغ." + Debt + "  می باشید.ابتدا از طریق لینک ''پرداخت بدهی آنلاین'' اقدام به تسویه حساب نمایید";
                return ReturnValue;
            }

            string Loan = Utility.CheckMemberLoanDebt(MeId);
            if (Loan == "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطا در ارتباط با وب سرویس دریافت میزان بدهی اقساط وام ایجاد شده است.";
                return ReturnValue;
            }
            if (Loan != "0" && Loan != "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "شما دارای بدهی وام به مبلغ." + Loan + "  می باشید.ابتدا از طریق لینک ''پرداخت آنلاین اقساط وام'' اقدام به تسویه حساب نمایید";
                return ReturnValue;
            }

            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
            ReqManager.FindByMemberId(MeId, 0, -1);
            if (ReqManager.Count > 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "امکان ثبت درخواست صدور پروانه اشتغال به کار وجود ندارد.شما در واحد عضویت درخواست درجریان دارید که در حال بررسی می باشد.پس از تایید توسط کارشناسان واحد عضویت می توانید نسبت به ثبت درخواست پروانه اشتغال خود اقدام نمایید. ";
                return ReturnValue;
            }
            TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
            DataTable dtMemberFileMajor = MemberLicenceManager.SelectByMemberId(MeId, 0);
            dtMemberFileMajor.DefaultView.RowFilter = "DefaultValue=1";
            if (dtMemberFileMajor.DefaultView.Count != 1)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "رشته تحصیلی پیش فرض در واحد عضویت برای شما نامشخص می باشد.پیش از آن بایستی از طریق لینک ''درخواست تغییر مدرک تحصیلی'' موجود در همین صفحه اقدام به تکمیل اطلاعات خود نموده و پس از تایید توسط واحد عضویت مجدد درخواست صدور پروانه نمایید";
                return ReturnValue;
            }

            dtMemberFileMajor.DefaultView.RowFilter = "LicenceCode=" + (int)TSP.DataManager.Licence.KarshenasiNaPeyvaste + " or " + "LicenceCode=" + (int)TSP.DataManager.Licence.kardani;
            if (dtMemberFileMajor.DefaultView.Count > 0)
                HasKardan = false;
            else
                HasKardan = true;



            if (CurrentUser_LoginType == (int)TSP.DataManager.UserType.TemporaryMembers)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "تنها اعضای تایید شده سازمان قادر به درخواست صدور پروانه اشتغال به کار می باشند.";
                return ReturnValue;
            }
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DocMemberFileManager.FindByMeId(MeId);
            if (DocMemberFileManager.Count > 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "پیش از این درخواست پروانه برای شما در سیستم ثبت شده است.جهت اطلاع از وضعیت پروانه خود از طریق منوی ''واحد صدور پروانه>>مدیریت پروانه اشتغال به کار'' اقدام نمایید";
                return ReturnValue;
            }
            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
            int MReId = -2;

            MemberManager.FindByCode(MeId);
            if (MemberManager.Count != 1)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطایی در بازیابی اطلاعات ایجاد شده است";
                return ReturnValue;
            }
            if (Convert.ToInt32(MemberManager[0]["MrsId"]) != 1)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "امکان درخواست صدور پروانه اشتغال برای شما وجود ندارد.عضویت شما در وضعیت لغو شده یا در جریان می باشد.";
                return ReturnValue;
            }
            ReqManager.FindByMemberId(MeId, 0, 1, -1);
            if (ReqManager.Count <= 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطایی در بازیابی اطلاعات ایجاد شده است";
                return ReturnValue;
            }
            MReId = Convert.ToInt32(ReqManager[0]["MReId"]);
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
            {
                if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(MemberManager[0]["ImageUrl"].ToString())))
                    HasImage = true;
            }
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["SSN"]))
            {
                HasSSNValue = true;
            }

            if (!HasKardan)
            {
                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["NezamKardanConfirmURL"]))
                {
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(MemberManager[0]["NezamKardanConfirmURL"].ToString())))
                        HasKardan = true;
                }
            }
            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNo);
            if (attachManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(attachManager[0]["FilePath"]))
                {
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(attachManager[0]["FilePath"].ToString())))
                        HasIdNo = true;
                }
            }

            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSN);
            if (attachManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(attachManager[0]["FilePath"]))
                {
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(attachManager[0]["FilePath"].ToString())))
                        HasSSN = true;
                }
            }

            if (MemberManager[0]["SexId"].ToString() == "2")
            {
                attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                if (attachManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(attachManager[0]["FilePath"]))
                    {
                        if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(attachManager[0]["FilePath"].ToString())))
                            HasSoldire = true;
                    }
                }
            }
            else HasSoldire = true;

            if (!HasImage || !HasIdNo || !HasSSN || !HasSoldire|| !HasKardan)
            {
                string Message = "";
                if (!HasImage)
                    Message += " - تصویر شناسایی ";
                if (!HasIdNo)
                    Message += " - تصویر شناسنامه ";
                if (!HasSSN)
                    Message += " - تصویر کارت ملی ";
                if (!HasSoldire)
                    Message += " - تصویر کارت پایان خدمت ";
                if (!HasSSNValue)
                    Message += " - کد ملی ";
                if (!HasKardan)
                    Message += " - تصویر استعلام عدم عضویت در نظام کاردانی ";
                string Msg = "بدليل اينكه مدارك عضويت شما در سيستم بطور كامل آپلود نشده است مي بايستي ابتدا از طريق لينك ''درخواست تغييرات پرونده عضويت''، موجود در همين صفحه ، اقدام به تكميل اطلاعات خود نموده و پس از تائيد توسط كارمند واحد عضويت ،مجدد درخواست خود را در قسمت پروانه اشتغال ، انجام دهيد.مدارک ناقص عبارتند از ";
                Message = Msg + Message;
                //Message += "شما در سیستم ثبت نشده است.پیش از آن بایستی از طریق لینک ''درخواست تغییر اطلاعات پایه'' موجود در همین صفحه اقدام به تکمیل اطلاعات خود نموده و پس از تایید توسط واحد عضویت مجدد درخواست صدور پروانه نمایید";

                ReturnValue[0] = false;
                ReturnValue[1] = Message;
                return ReturnValue;
            }
            return ReturnValue;
        }


        /// <summary>
        /// چک کردن شرایط اولیه ثبت درخواست صدور پروانه اشتغال شخص حقیقی
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUser_LoginType"></param>
        /// <returns></returns>
        public static ArrayList CheckConditionForDocumentQualification(int MeId, int CurrentUser_LoginType)
        {
            ArrayList ReturnValue = new ArrayList();
            ReturnValue.Add(true);
            ReturnValue.Add("");
            Boolean HasImage = false, HasIdNo = false, HasSSN = false, HasSoldire = false, HasSSNValue = false, HasKardan = false;
            ArrayList MeReqResult = Utility.CheckMemberRequestVisibility();
            if (Convert.ToBoolean(MeReqResult[0]))
            {
                ReturnValue[0] = false;
                ReturnValue[1] = MeReqResult[1];
                return ReturnValue;
            }
            if (CurrentUser_LoginType == (int)TSP.DataManager.UserType.TemporaryMembers)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "تنها اعضای تایید شده سازمان قادر به درخواست درج صلاحیت جدید پروانه اشتغال به کار می باشند.";
                return ReturnValue;
            }
            string Debt = Utility.CheckMemberOfflineDebt(MeId);
            if (Debt == "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطا در ارتباط با وب سرویس دریافت میزان بدهی ایجاد شده است.";
                return ReturnValue;
            }
            if (Debt != "0" && Debt != "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "شما دارای بدهی به مبلغ." + Debt + "  می باشید.ابتدا از طریق لینک ''پرداخت بدهی آنلاین'' اقدام به تسویه حساب نمایید";
                return ReturnValue;
            }
            string Loan = Utility.CheckMemberLoanDebt(MeId);
            if (Loan == "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطا در ارتباط با وب سرویس دریافت میزان بدهی اقساط وام ایجاد شده است.";
                return ReturnValue;
            }
            if (Loan != "0" && Loan != "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "شما دارای بدهی وام به مبلغ." + Loan + "  می باشید.ابتدا از طریق لینک ''پرداخت آنلاین اقساط وام'' اقدام به تسویه حساب نمایید";
                return ReturnValue;
            }
            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
            ReqManager.FindByMemberId(MeId, 0, -1);
            if (ReqManager.Count > 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "امکان ثبت درخواست درج صلاحیت جدید پروانه اشتغال به کار وجود ندارد.شما در واحد عضویت درخواست درجریان دارید که در حال بررسی می باشد.پس از تایید توسط کارشناسان واحد عضویت می توانید نسبت به ثبت درخواست پروانه اشتغال خود اقدام نمایید. ";
                return ReturnValue;
            }
            TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
            DataTable dtMemberFileMajor = MemberLicenceManager.SelectByMemberId(MeId, 0);
            dtMemberFileMajor.DefaultView.RowFilter = "DefaultValue=1";
            if (dtMemberFileMajor.DefaultView.Count != 1)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "رشته تحصیلی پیش فرض در واحد عضویت برای شما نامشخص می باشد.پیش از آن بایستی از طریق لینک ''درخواست تغییر مدرک تحصیلی'' موجود در همین صفحه اقدام به تکمیل اطلاعات خود نموده و پس از تایید توسط واحد عضویت مجدد درخواست صدور پروانه نمایید";
                return ReturnValue;
            }
            dtMemberFileMajor.DefaultView.RowFilter = "LicenceCode=" + (int)TSP.DataManager.Licence.KarshenasiNaPeyvaste + " or " + "LicenceCode=" + (int)TSP.DataManager.Licence.kardani;
            if (dtMemberFileMajor.DefaultView.Count > 0)
                HasKardan = false;
            else
                HasKardan = true;
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DocMemberFileManager.SelectLastVersion(MeId, 0, -1);
            if (DocMemberFileManager.Count == 1 && Convert.ToInt32(DocMemberFileManager[0]["IsConfirm"]) == 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "قادر به ثبت درخواست درج صلاحیت جدید نمی باشید.شما دارای درخواست تایید نشده در واحد پروانه اشتغال می باشید.جهت اطلاع از وضعیت پروانه خود از طریق منوی ''واحد صدور پروانه>>مدیریت پروانه اشتغال به کار'' اقدام نمایید";
                return ReturnValue;
            }
            if ((DocMemberFileManager.Count == 1 && Convert.ToInt32(DocMemberFileManager[0]["IsConfirm"]) == 2 && (Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)DocumentOfMemberRequestType.New || Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)DocumentOfMemberRequestType.Transfer)) || DocMemberFileManager.Count <= 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "قادر به ثبت درخواست درج صلاحیت جدید نمی باشید.شما دارای پروانه اشتغال به کار تایید شده نمی باشید.در صورت داشتن شرایط از منوی ''درخواست صدور پروانه'' اقدام نمایید";
                return ReturnValue;
            }


            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
            int MReId = -2;

            MemberManager.FindByCode(MeId);
            if (MemberManager.Count != 1)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطایی در بازیابی اطلاعات ایجاد شده است";
                return ReturnValue;
            }
            if (Convert.ToInt32(MemberManager[0]["MrsId"]) != 1)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "امکان درخواست درج صلاحیت جدید در پروانه اشتغال برای شما وجود ندارد.عضویت شما در وضعیت لغو شده یا در جریان می باشد.";
                return ReturnValue;
            }
            ReqManager.FindByMemberId(MeId, 0, 1, -1);
            if (ReqManager.Count <= 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطایی در بازیابی اطلاعات ایجاد شده است";
                return ReturnValue;
            }
            MReId = Convert.ToInt32(ReqManager[0]["MReId"]);
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
            {
                if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(MemberManager[0]["ImageUrl"].ToString())))
                    HasImage = true;
            }
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["SSN"]))
            {
                HasSSNValue = true;
            }

            if (!HasKardan)
            {
                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["NezamKardanConfirmURL"]))
                {
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(MemberManager[0]["NezamKardanConfirmURL"].ToString())))
                        HasKardan = true;
                }
            }

            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNo);
            if (attachManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(attachManager[0]["FilePath"]))
                {
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(attachManager[0]["FilePath"].ToString())))
                        HasIdNo = true;
                }
            }

            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSN);
            if (attachManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(attachManager[0]["FilePath"]))
                {
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(attachManager[0]["FilePath"].ToString())))
                        HasSSN = true;
                }
            }

            if (MemberManager[0]["SexId"].ToString() == "2")
            {
                attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                if (attachManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(attachManager[0]["FilePath"]))
                    {
                        if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(attachManager[0]["FilePath"].ToString())))
                            HasSoldire = true;
                    }
                }
            }
            else HasSoldire = true;


            if (!HasImage || !HasIdNo || !HasSSN || !HasSoldire || !HasKardan)
            {
                string Message = "";
                if (!HasImage)
                    Message += " - تصویر شناسایی ";
                if (!HasIdNo)
                    Message += " - تصویر شناسنامه ";
                if (!HasSSN)
                    Message += " - تصویر کارت ملی ";
                if (!HasSoldire)
                    Message += " - تصویر کارت پایان خدمت ";
                if (!HasSSNValue)
                    Message += " - کد ملی ";
                if (!HasKardan)
                    Message += " - تصویر استعلام عدم عضویت در نظام کاردانی ";
                string Msg = "بدليل اينكه مدارك عضويت شما در سيستم بطور كامل آپلود نشده است مي بايستي ابتدا از طريق لينك ''درخواست تغييرات پرونده عضويت''، موجود در همين صفحه ، اقدام به تكميل اطلاعات خود نموده و پس از تائيد توسط كارمند واحد عضويت ،مجدد درخواست خود را در قسمت پروانه اشتغال ، انجام دهيد.مدارک ناقص عبارتند از ";
                Message = Msg + Message;
                //Message += "شما در سیستم ثبت نشده است.پیش از آن بایستی از طریق لینک ''درخواست تغییر اطلاعات پایه'' موجود در همین صفحه اقدام به تکمیل اطلاعات خود نموده و پس از تایید توسط واحد عضویت مجدد درخواست درج صلاحیت جدید در پروانه نمایید";
                ReturnValue[0] = false;
                ReturnValue[1] = Message;
                return ReturnValue;
            }
            return ReturnValue;
        }


        /// <summary>
        /// چک کردن شرایط اولیه ثبت درخواست تمدید پروانه اشتغال شخص حقیقی
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUser_LoginType"></param>
        /// <returns></returns>
        public static ArrayList CheckConditionForDocumentRevival(int MeId, int CurrentUser_LoginType)
        {
            ArrayList ReturnValue = new ArrayList();
            ReturnValue.Add(true);
            Boolean HasImage = false, HasIdNo = false, HasSSN = false, HasSoldire = false, HasSSNValue = false, HasKardan = false;
            ReturnValue.Add("");
            ArrayList MeReqResult = Utility.CheckMemberRequestVisibility();
            if (Convert.ToBoolean(MeReqResult[0]))
            {
                ReturnValue[0] = false;
                ReturnValue[1] = MeReqResult[1];
                return ReturnValue;
            }
            if (CurrentUser_LoginType == (int)TSP.DataManager.UserType.TemporaryMembers)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "تنها اعضای تایید شده سازمان قادر به درخواست تمدید پروانه اشتغال به کار می باشند.";
                return ReturnValue;
            }
            string Debt = Utility.CheckMemberOfflineDebt(MeId);
            if (Debt == "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطا در ارتباط با وب سرویس دریافت میزان بدهی ایجاد شده است.";
                return ReturnValue;
            }
            if (Debt != "0" && Debt != "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "شما دارای بدهی به مبلغ." + Debt + "  می باشید.ابتدا از طریق لینک ''پرداخت بدهی آنلاین'' اقدام به تسویه حساب نمایید";
                return ReturnValue;
            }
            string Loan = Utility.CheckMemberLoanDebt(MeId);
            if (Loan == "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطا در ارتباط با وب سرویس دریافت میزان بدهی اقساط وام ایجاد شده است.";
                return ReturnValue;
            }
            if (Loan != "0" && Loan != "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "شما دارای بدهی وام به مبلغ." + Loan + "  می باشید.ابتدا از طریق لینک ''پرداخت آنلاین اقساط وام'' اقدام به تسویه حساب نمایید";
                return ReturnValue;
            }
            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
            ReqManager.FindByMemberId(MeId, 0, -1);
            if (ReqManager.Count > 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "امکان ثبت درخواست تمدید پروانه اشتغال به کار وجود ندارد.شما در واحد عضویت درخواست درجریان دارید که در حال بررسی می باشد.پس از تایید توسط کارشناسان واحد عضویت می توانید نسبت به ثبت درخواست پروانه اشتغال خود اقدام نمایید. ";
                return ReturnValue;
            }
            TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
            DataTable dtMemberFileMajor = MemberLicenceManager.SelectByMemberId(MeId, 0);
            dtMemberFileMajor.DefaultView.RowFilter = "DefaultValue=1";
            if (dtMemberFileMajor.DefaultView.Count != 1)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "رشته تحصیلی پیش فرض در واحد عضویت برای شما نامشخص می باشد.پیش از آن بایستی از طریق لینک ''درخواست تغییر مدرک تحصیلی'' موجود در همین صفحه اقدام به تکمیل اطلاعات خود نموده و پس از تایید توسط واحد عضویت مجدد درخواست صدور پروانه نمایید";
                return ReturnValue;
            }

            dtMemberFileMajor.DefaultView.RowFilter = "LicenceCode=" + (int)TSP.DataManager.Licence.KarshenasiNaPeyvaste + " or " + "LicenceCode=" + (int)TSP.DataManager.Licence.kardani;
            if (dtMemberFileMajor.DefaultView.Count > 0)
                HasKardan = false;
            else
                HasKardan = true;
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DocMemberFileManager.SelectLastVersion(MeId, 0, -1);
            if (DocMemberFileManager.Count == 1 && Convert.ToInt32(DocMemberFileManager[0]["IsConfirm"]) == 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "قادر به ثبت درخواست تمدید نمی باشید.شما دارای درخواست تایید نشده در واحد پروانه اشتغال می باشید.جهت اطلاع از وضعیت پروانه خود از طریق منوی ''واحد صدور پروانه>>مدیریت پروانه اشتغال به کار'' اقدام نمایید";
                return ReturnValue;
            }
            if ((DocMemberFileManager.Count == 1 && Convert.ToInt32(DocMemberFileManager[0]["IsConfirm"]) == 2 && (Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)DocumentOfMemberRequestType.New || Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)DocumentOfMemberRequestType.Transfer)) || DocMemberFileManager.Count <= 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "قادر به ثبت درخواست تمدید نمی باشید.شما دارای پروانه اشتغال به کار تایید شده نمی باشید.در صورت داشتن شرایط از منوی ''درخواست صدور پروانه'' اقدام نمایید";
                return ReturnValue;
            }
            DocMemberFileManager.SelectLastVersion(MeId, 0, 1);
            if (DocMemberFileManager.Count == 1)
            {
                if (Utility.IsDBNullOrNullValue(DocMemberFileManager[0]["ExpireDate"]))
                {
                    ReturnValue[0] = false;
                    ReturnValue[1] = "تاریخ اعتبار پروانه شما مشخص نمی باشد.";
                    return ReturnValue;
                }
                string LastMonth = Utility.AddMonths(DocMemberFileManager[0]["ExpireDate"].ToString(), -2);
                string Today = Utility.GetDateOfToday();
                int IsDocExp = string.Compare(Today, LastMonth);
                if (IsDocExp <= 0)
                {
                    ReturnValue[0] = false;
                    ReturnValue[1] = "تاریخ اعتبار شما به پایان نرسیده است.از دو ماه قبل از به پایان رسیدن اعتبار پروانه قادر به ثبت درخواست تمدید می باشید.";
                    return ReturnValue;
                }
            }

            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
            int MReId = -2;

            MemberManager.FindByCode(MeId);
            if (MemberManager.Count != 1)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطایی در بازیابی اطلاعات ایجاد شده است";
                return ReturnValue;
            }
            if (Convert.ToInt32(MemberManager[0]["MrsId"]) != 1)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "امکان درخواست تمدید پروانه اشتغال برای شما وجود ندارد.عضویت شما در وضعیت لغو شده یا در جریان می باشد.";
                return ReturnValue;
            }
            ReqManager.FindByMemberId(MeId, 0, 1, -1);
            if (ReqManager.Count <= 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطایی در بازیابی اطلاعات ایجاد شده است";
                return ReturnValue;
            }
            MReId = Convert.ToInt32(ReqManager[0]["MReId"]);
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
            {
                if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(MemberManager[0]["ImageUrl"].ToString())))
                    HasImage = true;
            }
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["SSN"]))
            {
                HasSSNValue = true;
            }
            if (!HasKardan)
            {
                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["NezamKardanConfirmURL"]))
                {
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(MemberManager[0]["NezamKardanConfirmURL"].ToString())))
                        HasKardan = true;
                }
            }
            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNo);
            if (attachManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(attachManager[0]["FilePath"]))
                {
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(attachManager[0]["FilePath"].ToString())))
                        HasIdNo = true;
                }
            }

            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSN);
            if (attachManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(attachManager[0]["FilePath"]))
                {
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(attachManager[0]["FilePath"].ToString())))
                        HasSSN = true;
                }
            }

            if (MemberManager[0]["SexId"].ToString() == "2")
            {
                attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                if (attachManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(attachManager[0]["FilePath"]))
                    {
                        if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(attachManager[0]["FilePath"].ToString())))
                            HasSoldire = true;
                    }
                }
            }
            else HasSoldire = true;

            if (!HasImage || !HasIdNo || !HasSSN || !HasSoldire|| !HasKardan)
            {
                string Message = "";
                if (!HasImage)
                    Message += " - تصویر شناسایی ";
                if (!HasIdNo)
                    Message += " - تصویر شناسنامه ";
                if (!HasSSN)
                    Message += " - تصویر کارت ملی ";
                if (!HasSoldire)
                    Message += " - تصویر کارت پایان خدمت ";
                if (!HasSSNValue)
                    Message += " - کد ملی ";
                if (!HasKardan)
                    Message += " - تصویر استعلام عدم عضویت در نظام کاردانی ";
                string Msg = "بدليل اينكه مدارك عضويت شما در سيستم بطور كامل آپلود نشده است مي بايستي ابتدا از طريق لينك ''درخواست تغييرات پرونده عضويت''، موجود در همين صفحه ، اقدام به تكميل اطلاعات خود نموده و پس از تائيد توسط كارمند واحد عضويت ،مجدد درخواست خود را در قسمت پروانه اشتغال ، انجام دهيد.مدارک ناقص عبارتند از ";
                Message = Msg + Message;
                //Message += "شما در سیستم ثبت نشده است.پیش از آن بایستی از طریق لینک ''درخواست تغییر اطلاعات پایه'' موجود در همین صفحه اقدام به تکمیل اطلاعات خود نموده و پس از تایید توسط واحد عضویت مجدد درخواست تمدید پروانه نمایید";
                ReturnValue[0] = false;
                ReturnValue[1] = Message;
                return ReturnValue;
            }
            return ReturnValue;
        }


        /// <summary>
        /// چک کردن شرایط اولیه ثبت درخواست ارتقا پایه پروانه اشتغال شخص حقیقی
        /// </summary>
        /// <param name="MeId"></param>
        /// <param name="CurrentUser_LoginType"></param>
        /// <returns></returns>
        public static ArrayList CheckConditionForDocumentUpgrade(int MeId, int CurrentUser_LoginType)
        {
            ArrayList ReturnValue = new ArrayList();
            ReturnValue.Add(true);
            Boolean HasImage = false, HasIdNo = false, HasSSN = false, HasSoldire = false, HasSSNValue = false, HasKardan = false;
            ReturnValue.Add("");
            ArrayList MeReqResult = Utility.CheckMemberRequestVisibility();
            if (Convert.ToBoolean(MeReqResult[0]))
            {
                ReturnValue[0] = false;
                ReturnValue[1] = MeReqResult[1];
                return ReturnValue;
            }
            if (CurrentUser_LoginType == (int)TSP.DataManager.UserType.TemporaryMembers)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "تنها اعضای تایید شده سازمان قادر به درخواست تمدید پروانه اشتغال به کار می باشند.";
                return ReturnValue;
            }
            string Debt = Utility.CheckMemberOfflineDebt(MeId);
            if (Debt == "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطا در ارتباط با وب سرویس دریافت میزان بدهی ایجاد شده است.";
                return ReturnValue;
            }
            if (Debt != "0" && Debt != "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "شما دارای بدهی به مبلغ." + Debt + "  می باشید.ابتدا از طریق لینک ''پرداخت بدهی آنلاین'' اقدام به تسویه حساب نمایید";
                return ReturnValue;
            }
            string Loan = Utility.CheckMemberLoanDebt(MeId);
            if (Loan == "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطا در ارتباط با وب سرویس دریافت میزان بدهی اقساط وام ایجاد شده است.";
                return ReturnValue;
            }
            if (Loan != "0" && Loan != "-1")
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "شما دارای بدهی وام به مبلغ." + Loan + "  می باشید.ابتدا از طریق لینک ''پرداخت آنلاین اقساط وام'' اقدام به تسویه حساب نمایید";
                return ReturnValue;
            }
            TSP.DataManager.MemberRequestManager ReqManager = new TSP.DataManager.MemberRequestManager();
            ReqManager.FindByMemberId(MeId, 0, -1);
            if (ReqManager.Count > 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "امکان ثبت درخواست تمدید پروانه اشتغال به کار وجود ندارد.شما در واحد عضویت درخواست درجریان دارید که در حال بررسی می باشد.پس از تایید توسط کارشناسان واحد عضویت می توانید نسبت به ثبت درخواست پروانه اشتغال خود اقدام نمایید. ";
                return ReturnValue;
            }
            TSP.DataManager.MemberLicenceManager MemberLicenceManager = new TSP.DataManager.MemberLicenceManager();
            DataTable dtMemberFileMajor = MemberLicenceManager.SelectByMemberId(MeId, 0);
            dtMemberFileMajor.DefaultView.RowFilter = "DefaultValue=1";
            if (dtMemberFileMajor.DefaultView.Count != 1)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "رشته تحصیلی پیش فرض در واحد عضویت برای شما نامشخص می باشد.پیش از آن بایستی از طریق لینک ''درخواست تغییر مدرک تحصیلی'' موجود در همین صفحه اقدام به تکمیل اطلاعات خود نموده و پس از تایید توسط واحد عضویت مجدد درخواست صدور پروانه نمایید";
                return ReturnValue;
            }


            dtMemberFileMajor.DefaultView.RowFilter = "LicenceCode=" + (int)TSP.DataManager.Licence.KarshenasiNaPeyvaste + " or " + "LicenceCode=" + (int)TSP.DataManager.Licence.kardani;
            if (dtMemberFileMajor.DefaultView.Count > 0)
                HasKardan = false;
            else
                HasKardan = true;
            TSP.DataManager.DocMemberFileManager DocMemberFileManager = new TSP.DataManager.DocMemberFileManager();
            DocMemberFileManager.SelectLastVersion(MeId, 0, -1);
            if (DocMemberFileManager.Count == 1 && Convert.ToInt32(DocMemberFileManager[0]["IsConfirm"]) == 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "قادر به ثبت درخواست ارتقا پایه نمی باشید.شما دارای درخواست تایید نشده در واحد پروانه اشتغال می باشید.جهت اطلاع از وضعیت پروانه خود از طریق منوی ''واحد صدور پروانه>>مدیریت پروانه اشتغال به کار'' اقدام نمایید";
                return ReturnValue;
            }
            if ((DocMemberFileManager.Count == 1 && Convert.ToInt32(DocMemberFileManager[0]["IsConfirm"]) == 2 && (Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)DocumentOfMemberRequestType.New || Convert.ToInt32(DocMemberFileManager[0]["Type"]) == (int)DocumentOfMemberRequestType.Transfer)) || DocMemberFileManager.Count <= 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "قادر به ثبت درخواست ارتقا پایه نمی باشید.شما دارای پروانه اشتغال به کار تایید شده نمی باشید.در صورت داشتن شرایط از منوی ''درخواست صدور پروانه'' اقدام نمایید";
                return ReturnValue;
            }

            TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
            TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
            int MReId = -2;

            MemberManager.FindByCode(MeId);
            if (MemberManager.Count != 1)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطایی در بازیابی اطلاعات ایجاد شده است";
                return ReturnValue;
            }
            if (Convert.ToInt32(MemberManager[0]["MrsId"]) != 1)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "امکان درخواست ارتقا پایه پروانه اشتغال برای شما وجود ندارد.عضویت شما در وضعیت لغو شده یا در جریان می باشد.";
                return ReturnValue;
            }
            ReqManager.FindByMemberId(MeId, 0, 1, -1);
            if (ReqManager.Count <= 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "خطایی در بازیابی اطلاعات ایجاد شده است";
                return ReturnValue;
            }
            MReId = Convert.ToInt32(ReqManager[0]["MReId"]);
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["ImageUrl"]))
            {
                if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(MemberManager[0]["ImageUrl"].ToString())))
                    HasImage = true;
            }
            if (!Utility.IsDBNullOrNullValue(MemberManager[0]["SSN"]))
            {
                HasSSNValue = true;
            }
            if (!HasKardan)
            {
                if (!Utility.IsDBNullOrNullValue(MemberManager[0]["NezamKardanConfirmURL"]))
                {
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(MemberManager[0]["NezamKardanConfirmURL"].ToString())))
                        HasKardan = true;
                }
            }            

            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.IdNo);
            if (attachManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(attachManager[0]["FilePath"]))
                {
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(attachManager[0]["FilePath"].ToString())))
                        HasIdNo = true;
                }
            }

            attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SSN);
            if (attachManager.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(attachManager[0]["FilePath"]))
                {
                    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(attachManager[0]["FilePath"].ToString())))
                        HasSSN = true;
                }
            }

            if (MemberManager[0]["SexId"].ToString() == "2")
            {
                attachManager.FindByTablePrimaryKey_AttId(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.MemberRequest), MReId, (short)TSP.DataManager.AttachType.SoldierCard);
                if (attachManager.Count > 0)
                {
                    if (!Utility.IsDBNullOrNullValue(attachManager[0]["FilePath"]))
                    {
                        if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(attachManager[0]["FilePath"].ToString())))
                            HasSoldire = true;
                    }
                }
            }
            else HasSoldire = true;

            if (!HasImage || !HasIdNo || !HasSSN || !HasSoldire|| !HasKardan)
            {
                string Message = "";
                if (!HasImage)
                    Message += " - تصویر شناسایی ";
                if (!HasIdNo)
                    Message += " - تصویر شناسنامه ";
                if (!HasSSN)
                    Message += " - تصویر کارت ملی ";
                if (!HasSoldire)
                    Message += " - تصویر کارت پایان خدمت ";
                if (!HasSSNValue)
                    Message += " - کد ملی ";
                if (!HasKardan)
                    Message += " - تصویر استعلام عدم عضویت در نظام کاردانی ";
                string Msg = "بدليل اينكه مدارك عضويت شما در سيستم بطور كامل آپلود نشده است مي بايستي ابتدا از طريق لينك ''درخواست تغييرات پرونده عضويت''، موجود در همين صفحه ، اقدام به تكميل اطلاعات خود نموده و پس از تائيد توسط كارمند واحد عضويت ،مجدد درخواست خود را در قسمت پروانه اشتغال ، انجام دهيد.مدارک ناقص عبارتند از ";
                Message = Msg + Message;
                ReturnValue[0] = false;
                ReturnValue[1] = Message;
                return ReturnValue;
            }
            return ReturnValue;
        }


        /// <summary>
        /// پایه 3 به 2 نیاز به 4 سال سابقه کار
        ///پایه 2 به 1 نیاز به 5 سال سابقه کار
        /// </summary>
        /// <returns></returns>
        public ArrayList CheckUpgradeConditionsDateAndPeriodsForUpgrade(int MeId, string RequestDate)
        {
            ArrayList ReturnValue = new ArrayList(3);
            string Msg = "عضو محترم شما قادر به ثبت درخواست ارتقاء پایه نمی باشید. ";
            Boolean CanUpgrade = false;
            string listUpGrdId = "";
            string listUpGrdName = "";
            ReturnValue.Add(false);
            ReturnValue.Add("");
            ReturnValue.Add("");
            ReturnValue.Add("");


            TSP.DataManager.DocMemberFileDetailManager DocMemberFileDetailManager = new TSP.DataManager.DocMemberFileDetailManager();
            TSP.DataManager.DocUpGradePointManager DocUpGradePointManager = new TSP.DataManager.DocUpGradePointManager();
            TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
            DataTable dtRes = DocMemberFileDetailManager.SelectMeFileMaxGradForAllResponsiblity(MeId, -1);
            if (dtRes.Rows.Count == 0)
            {
                ReturnValue[0] = false;
                ReturnValue[1] = "پایه های اخذ شده توسط شما در سیستم تعریف نشده است. لطفا به واحد پروانه مراجعه نمایید";
                return ReturnValue;
            }
            int MjId = -2;
            string ResDate = "";
            for (int i = 0; i < dtRes.Rows.Count; i++)
            {

                MjId = Convert.ToInt32(dtRes.Rows[i]["FMjId"]);
                ResDate = dtRes.Rows[i]["Date"].ToString();
                switch (Convert.ToInt32(dtRes.Rows[i]["GrdId"]))
                {
                    case (int)TSP.DataManager.DocumentGrads.Grade1:
                        CanUpgrade = false;
                        Msg += "پایه یک " + dtRes.Rows[i]["ResName"].ToString() + " بالاترین پایه در این صلاحیت است" +
                            ".";
                        break;
                    case (int)TSP.DataManager.DocumentGrads.Grade2:
                        CanUpgrade = true;

                        if (string.Compare(Utility.AddMonths(Utility.GetDateOfToday(), -60), ResDate) < 0)
                        {
                            Msg += "با توجه به عدم گذشت 5 سال از پایه 2 " + dtRes.Rows[i]["ResName"].ToString() + " شما مجوز ثبت درخواست ارتقاء پایه ندارید";
                            CanUpgrade = false;
                        }

                        break;
                    case (int)TSP.DataManager.DocumentGrads.Grade3:
                        CanUpgrade = true;
                        if (string.Compare(Utility.AddMonths(Utility.GetDateOfToday(), -48), ResDate) < 0)
                        {
                            Msg += "با توجه به عدم گذشت 4 سال از پایه 3" + dtRes.Rows[i]["ResName"].ToString() + " شما مجوز ثبت درخواست ارتقاء پایه ندارید";
                            CanUpgrade = false;
                        }
                        break;
                }
                if (CanUpgrade)
                {
                    #region CheckPeriods
                    DataTable dtUpgradePoint = DocUpGradePointManager.SelectActiveUpgradePoint(Convert.ToInt32(dtRes.Rows[i]["GrdId"]), Convert.ToInt32(dtRes.Rows[i]["MjParentId"]), Convert.ToInt32(dtRes.Rows[i]["ResId"]));
                    if (dtUpgradePoint.Rows.Count > 0)
                    {
                        int MinPeriodNeed = Convert.ToInt32(dtUpgradePoint.Rows[0]["MinPeriodNeed"]);
                        DataTable dtPeriodRegister = PeriodRegisterManager.selectPeriodRegisterForMemberReport(MeId, Convert.ToInt32(dtRes.Rows[i]["MjParentId"]), Convert.ToInt32(dtRes.Rows[i]["ResId"]), Convert.ToInt32(dtRes.Rows[i]["GrdId"]), 1, RequestDate);
                        if (dtPeriodRegister.Rows.Count < MinPeriodNeed)
                        {
                            Msg = "جهت ارتقا " + dtRes.Rows[i]["GrdName"].ToString() + " " + dtRes.Rows[i]["ResName"].ToString() + " گذراندن حداقل " + MinPeriodNeed.ToString() + " دوره آموزشی الزامی می باشد.جهت مشاهده لیست  دوره های مورد نیاز جهت ارتقاء پایه به لینک مربوطه در همین صفحه مراجعه نمایید ";
                            CanUpgrade = false;
                        }
                        else
                        {
                            CanUpgrade = true;
                            listUpGrdId += dtUpgradePoint.Rows[0]["UpGrdId"].ToString() + ";";
                            listUpGrdName += dtUpgradePoint.Rows[0]["UpGrageFullName"].ToString() + ";";
                        }
                    }
                    else
                    {
                        CanUpgrade = false;
                    }
                    #endregion
                }
                if (CanUpgrade)
                    ReturnValue[0] = CanUpgrade;
            }
            //ReturnValue[0] = CanUpgrade;
            ReturnValue[1] = Msg;
            ReturnValue[2] = listUpGrdId;
            ReturnValue[3] = listUpGrdName;
            return ReturnValue;
        }
        #endregion

        #region ShouldBeChanged
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMainRequest(int MeId, int DocType, String FollowCode, String EndDateFrom, String EndDateTo, String FirstName, String LastName, String MFNo, int LastConfirmReqType, string MFNoWithOutSerial, int MFSerialNo, int TaskCodeAccConf, int TaskCode, Int16 MeDataComplete, Int16 MeDocComplete, string WFDate, string WFDoerName, Int16 PaymentType, Int16 PaymentStatus, string CreateDateLastRequst, Int16 TaskId)
        {

            return SelectMainRequest(MeId, DocType, FollowCode, EndDateFrom, EndDateTo, FirstName, LastName, MFNo, LastConfirmReqType, MFNoWithOutSerial, MFSerialNo, TaskCodeAccConf, TaskCode, MeDataComplete, MeDocComplete, WFDate, WFDoerName, PaymentType, PaymentStatus, CreateDateLastRequst, TaskId, true, "2", "1", "2", -1, -1, -1, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMainRequest(int MeId, int DocType, String FollowCode, String EndDateFrom, String EndDateTo, String FirstName, String LastName, String MFNo, int LastConfirmReqType
            , string MFNoWithOutSerial, int MFSerialNo, int TaskCodeAccConf, int TaskCode)
        {
            return SelectMainRequest(MeId, DocType, FollowCode, EndDateFrom, EndDateTo, FirstName, LastName, MFNo, LastConfirmReqType
                , MFNoWithOutSerial, MFSerialNo, TaskCodeAccConf, TaskCode, -1, -1, "1", "%", -1, -1, "1", -1);
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMainRequest(String FollowCode)
        {
            return SelectMainRequest(-1, 0, FollowCode, "1", "2", "%", "%", "%", -1, "%", -1, -1, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMainRequest(int MeId, int DocType, String FollowCode, String EndDateFrom, String EndDateTo, String FirstName, String LastName, String MFNo, int LastConfirmReqType
            , string MFNoWithOutSerial, int MFSerialNo, int TaskCodeAccConf)
        {
            return SelectMainRequest(MeId, DocType, FollowCode, EndDateFrom, EndDateTo, FirstName, LastName, MFNo, LastConfirmReqType, MFNoWithOutSerial, MFSerialNo, TaskCodeAccConf, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMainRequest(int MeId, int DocType, String FollowCode, String EndDateFrom, String EndDateTo, String FirstName, String LastName, String MFNo, int LastConfirmReqType
            , string MFNoWithOutSerial, int MFSerialNo)
        {
            return SelectMainRequest(MeId, DocType, FollowCode, EndDateFrom, EndDateTo, FirstName, LastName, MFNo, LastConfirmReqType, MFNoWithOutSerial, MFSerialNo, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMainRequest(int MeId, int DocType, String FollowCode, String EndDateFrom, String EndDateTo, String FirstName, String LastName, String MFNo, int LastConfirmReqType)
        {
            return SelectMainRequest(MeId, DocType, FollowCode, EndDateFrom, EndDateTo, FirstName, LastName, MFNo, LastConfirmReqType, "%", -1);

        }


        //////[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        //////public DataTable SelectMainRequest(int MeId, int DocType, String FollowCode, String EndDateFrom, String EndDateTo, String FirstName, String LastName, String MFNo, int LastConfirmReqType, Int16 MeDataComplete, Int16 MeDocComplete, string WFDate, string WFDoerName, Int16 PaymentType, Int16 PaymentStatus, string CreateDateLastRequst, Int16 TaskId)
        //////{
        //////    return SelectMainRequest(MeId, DocType, FollowCode, EndDateFrom, EndDateTo, FirstName, LastName, MFNo, LastConfirmReqType, "%", -1, -1, -1, MeDataComplete, MeDocComplete, WFDate, WFDoerName, PaymentType, PaymentStatus, CreateDateLastRequst, TaskId, true);

        //////}

        //--------------

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMainRequest(int MeId, int DocType, String FollowCode, String EndDateFrom, String EndDateTo, String FirstName, String LastName, String MFNo)
        {
            return SelectMainRequest(MeId, DocType, FollowCode, EndDateFrom, EndDateTo, FirstName, LastName, MFNo, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMainRequestByMfNo(String MFNo, int DocType)
        {
            return SelectMainRequest(-1, DocType, "%", "1", "2", "%", "%", MFNo, -1);
        }


        public DataTable SearchMemberFileBySepratedMfNo(string MFNoWithOutSerial, int MFSerialNo)
        {
            return SelectMainRequest(-1, 0, "%", "1", "2", "%", "%", "%", -1, MFNoWithOutSerial, MFSerialNo);

        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMainRequest(int TaskCode)
        {
            return SelectMainRequest(-1, 0, "%", "1", "2", "%", "%", "%", -1, "%", -1, -1, TaskCode);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectMainRequest(int MeId, int DocType)
        {

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocMemberFileMainRequest", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4);
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@DocType", SqlDbType.Int, 4).Value = DocType;
            if (DocType == 0)
                adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.DocumentOfMemberConfirming;
            else if (DocType == 1)
                adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.ImplementDocumentConfirming;
            else if (DocType == 2)
                adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.ObservationDocumentConfirming;

            adapter.Fill(DataTable);
            return (DataTable);
        }


        public static Boolean IsMemmeberDocumentInSettlementstate(int MeId)
        {
            DocMemberFileManager DocMemberFileManager = new DataManager.DocMemberFileManager();
            DataTable dtDocMe = DocMemberFileManager.SelectMainRequest(MeId, 0);
            if (dtDocMe.Rows.Count > 0)
            {
                if (!Utility.IsDBNullOrNullValue(dtDocMe.Rows[0]["TaskCode"])
                   && (
                   Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) == (int)TSP.DataManager.WorkFlowTask.ResponsibleOfDocumentUnitEmployeeConfirmingDocument
                   || Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) == (int)TSP.DataManager.WorkFlowTask.settlementAgentConfirmingDocument
                   || Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) == (int)TSP.DataManager.WorkFlowTask.NezamEmployeeInSettlementConfirmingDocument
                    || Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) == (int)TSP.DataManager.WorkFlowTask.ModireMaskanConfirmatingDocument
                   || Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) == (int)TSP.DataManager.WorkFlowTask.RoadAndurbanismConfirmingDocument
                   || Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) == (int)TSP.DataManager.WorkFlowTask.PrintDocumentByNezamEmployee
                   || Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) == (int)TSP.DataManager.WorkFlowTask.PrintAndWaitingForConfirm
                   || Convert.ToInt32(dtDocMe.Rows[0]["TaskCode"]) == (int)TSP.DataManager.WorkFlowTask.DocumentOfMemberConfirmedAndWaitForSendingToNezam
                   ))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
        public static List<DataClasses.ReportMemberWithoutDocByMajorResult> ReportMemberWithoutDocByMajor()
        {
            DataClasses.DataClassesDataContext DataContext = new DataClasses.DataClassesDataContext();
            return DataContext.ReportMemberWithoutDocByMajor((int)SexManager.Sex.Female, (int)SexManager.Sex.Male, (int)MainMajors.Architecture, (int)MainMajors.Urbanism, (int)MainMajors.Civil, (int)MainMajors.Mechanic, (int)MainMajors.Electronic
                , (int)MainMajors.Mapping, (int)MainMajors.Traffic).ToList();
        }

        public static List<DataClasses.ReportMemberWithDocByMajorResult> ReportMemberWithDocByMajor(int SexId, int MajorParentId)
        {
            DataClasses.DataClassesDataContext DataContext = new DataClasses.DataClassesDataContext();
            return DataContext.ReportMemberWithDocByMajor(TableTypeManager.FindTtId(TableType.DocMemberFileMajor), TableTypeManager.FindTtId(TableType.DocMemberFileDetail), SexId, (int)DocumentGrads.Grade3, (int)DocumentGrads.Grade2, (int)DocumentGrads.Grade1, (int)DocumentGrads.Arshad, MajorParentId).ToList();
        }
        #endregion

        #region ImplementDoc Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId">MeId</param>
        /// <param name="MfId">ImpDocId</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectImplementDoc(int MeId, int MfId)
        {

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocMemberFileForImplementDoc", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4).Value = MfId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4).Value = -1;
            adapter.SelectCommand.Parameters.Add("@TaskId", SqlDbType.Int, 4).Value = -1;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.ImplementDocumentConfirming;

            adapter.Fill(DataTable);
            return (DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectImpDocSubRequest(int MeId, int TaskCode)
        {

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocMemberFileSubImpDocRequest", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4).Value = TaskCode;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4).Value = -1;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.ImplementDocumentConfirming;

            adapter.Fill(DataTable);
            return (DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectImpDocMainRequest(int MeId)
        {

            return SelectImpDocMainRequest(MeId, "%", "1", "2", "1", "2");
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectImpDocMainRequest(int MeId, String FollowCode, String EndDateFrom, String EndDateTo)
        {
            return SelectImpDocMainRequest(MeId, FollowCode, EndDateFrom, EndDateTo, "1", "2");
        }
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectImpDocMainRequest(int MeId, String FollowCode, String EndDateFrom, String EndDateTo, String ReqCreateDateFrom, String ReqCreateDateTo)
        {

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocMemberFileMainImpDocRequest", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4);
            adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4);
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.ImplementDocumentConfirming;
            adapter.SelectCommand.Parameters.AddWithValue("@FollowCode", FollowCode);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDateFrom", EndDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDateTo", EndDateTo);
            adapter.SelectCommand.Parameters.AddWithValue("@ReqCreateDateFrom", ReqCreateDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@ReqCreateDateTo", ReqCreateDateTo);

            adapter.Fill(DataTable);
            return (DataTable);
        }


        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectImpDocLastVersion(int MeId, int MfId, int IsConfirm, int InActive)
        {

            SqlDataAdapter adapter = this.Adapter;
            DataTable.Clear();
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileImpDocLastVersion";
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@MeId", MeId);
            adapter.SelectCommand.Parameters.AddWithValue("@MfId", MfId);
            adapter.SelectCommand.Parameters.AddWithValue("@IsConfirm", IsConfirm);
            adapter.SelectCommand.Parameters.AddWithValue("@InActive", InActive);
            adapter.Fill(DataTable);
            return (DataTable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId">MeId</param>
        /// <param name="MfId">Id of DocMemberFile.When DocumentType is ImplementDoc MeId in Doc.MemberFile equal by MfId</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectImpDocLastVersion(int MeId, int MfId)
        {
            return SelectImpDocLastVersion(MeId, MfId, -1, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId">MeId</param>
        /// <param name="MfId">Id of DocMemberFile.When DocumentType is ImplementDoc MeId in Doc.MemberFile equal by MfId</param>
        /// <param name="IsConfirm">IsConfirm</param>
        /// <returns></returns> 
        public DataTable SelectImpDocLastVersion(int MeId, int MfId, int IsConfirm)
        {
            return SelectImpDocLastVersion(MeId, MfId, IsConfirm, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId">MeId</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectImpDocLastVersionByMeId(int MeId)
        {
            return (SelectImpDocLastVersion(MeId, -1));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MfId">Id of DocMemberFile.When DocumentType is ImplementDoc MeId in Doc.MemberFile equal by MfId</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectImpDocLastVersionByMeFileId(int MfId)
        {
            return (SelectImpDocLastVersion(-1, MfId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MfId">Id of DocMemberFile.When DocumentType is ImplementDoc MeId in Doc.MemberFile equal by MfId</param>
        ///   /// <param name="IsConfirm">IsConfirm</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectImpDocLastVersionByMeFileId(int MfId, int IsConfirm)
        {
            return (SelectImpDocLastVersion(-1, MfId, IsConfirm));
        }
        #endregion

        #region ObservationDoc Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId">MeId</param>
        /// <param name="MfId">ImpDocId</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectObservationDoc(int MeId, int MfId)
        {

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocMemberFileForObservationDoc", this.Connection);
            DataTable.Clear();
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4).Value = MfId;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4).Value = -1;
            adapter.SelectCommand.Parameters.Add("@TaskId", SqlDbType.Int, 4).Value = -1;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.ObservationDocumentConfirming;

            adapter.Fill(DataTable);
            return (DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectObsDocMainRequest(int MeId)
        {

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocMemberFileMainObsDocRequest", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4);
            adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4);
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.ObservationDocumentConfirming;

            adapter.Fill(DataTable);
            return (DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectObsDocMainRequest(int MeId, String FollowCode, String EndDateFrom, String EndDateTo)
        {

            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocMemberFileMainObsDocRequest", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4);
            adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4);
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.ObservationDocumentConfirming;
            adapter.SelectCommand.Parameters.AddWithValue("@FollowCode", FollowCode);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDateFrom", EndDateFrom);
            adapter.SelectCommand.Parameters.AddWithValue("@EndDateTo", EndDateTo);

            adapter.Fill(DataTable);
            return (DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectObsDocSubRequest(int MeId, int TaskCode, int MfId)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("spSelectDocMemberFileSubObsDocRequest", this.Connection);
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@TaskCode", SqlDbType.Int, 4).Value = TaskCode;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4).Value = MfId;
            adapter.SelectCommand.Parameters.Add("@WorkFlowCode", SqlDbType.Int, 4).Value = WorkFlows.ObservationDocumentConfirming;

            adapter.Fill(DataTable);
            return (DataTable);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectObsDocSubRequest(int MeId, int TaskCode)
        {
            return SelectObsDocSubRequest(MeId, TaskCode, -1);
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectObsDocSubRequest(int MfId)
        {
            return SelectObsDocSubRequest(MfId, -1, -1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId">MeId</param>
        /// <param name="MfId">Id of DocMemberFile.When DocumentType is ImplementDoc or ObservationDoc MeId in Doc.MemberFile equal by MfId</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectObsDocLastVersion(int MeId, int MfId)
        {
            SqlDataAdapter adapter = this.Adapter;
            DataTable.Clear();
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileObsDocLastVersion";
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4).Value = MfId;
            adapter.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.Int, 4);

            adapter.Fill(DataTable);
            return (DataTable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId">MeId</param>
        /// <param name="MfId">Id of DocMemberFile.When DocumentType is ImplementDoc or ObservationDoc MeId in Doc.MemberFile equal by MfId</param>
        /// <param name="IsConfirm"></param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectObsDocLastVersion(int MeId, int MfId, int IsConfirm)
        {
            SqlDataAdapter adapter = this.Adapter;
            DataTable.Clear();
            adapter.SelectCommand.Transaction = this.Transaction;
            adapter.SelectCommand.CommandText = "spSelectDocMemberFileObsDocLastVersion";
            adapter.SelectCommand.Parameters.Clear();
            adapter.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@MeId", SqlDbType.Int, 4).Value = MeId;
            adapter.SelectCommand.Parameters.Add("@MfId", SqlDbType.Int, 4).Value = MfId;
            adapter.SelectCommand.Parameters.Add("@IsConfirm", SqlDbType.Int, 4).Value = IsConfirm;

            adapter.Fill(DataTable);
            return (DataTable);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId">MeId</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectObsDocLastVersionByMeId(int MeId)
        {
            return (SelectObsDocLastVersion(MeId, -1));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="MfId">Id of DocMemberFile.When DocumentType is ImplementDoc or ObservationDoc MeId in Doc.MemberFile equal by MfId</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectObsDocLastVersionByMeFileId(int MfId)
        {
            return (SelectObsDocLastVersion(-1, MfId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MeId">MeId</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectObsDocLastVersionByMeId(int MeId, int IsConfirm)
        {
            return (SelectObsDocLastVersion(MeId, -1, IsConfirm));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="MfId">Id of DocMemberFile.When DocumentType is ImplementDoc or ObservationDoc MeId in Doc.MemberFile equal by MfId</param>
        /// <returns></returns>
        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable SelectObsDocLastVersionByMeFileId(int MfId, int IsConfirm)
        {
            return (SelectObsDocLastVersion(-1, MfId, IsConfirm));
        }
        #endregion
    }
}

