using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace TSP.DataManager
{
    public enum AccountingErrors
    {
        Succed = 0, ErrorInSave = -1, ErrorInFill = -2, InvalidDocNumber = -3
    }

    public enum AccountingDocumentRelocationType
    {
        InsteadOf = 0,
        After = 1
    }
    public class AccountingDocument
    {
        #region Parameters
        private int AgentCode, UserCode, PrjId, CCId, TableTypeId, TTId;
        private string PerDate, ActionMode, Des, BankDocNum;
        private string ImageURL, RefName;
        private int DocOperationId;
        private int DocBalanceId;
        private int HId;
        private int DocNumber;
        private int DirectAccess, DocStatusId;
        private int IsMerged, IsBalanced;
        #endregion

        #region Managers
        private TSP.DataManager.AccountingDocOperationManager docOpManager;
        private TSP.DataManager.AccountingDocOperationManager docOperationManager;
        private TSP.DataManager.AccountingDocBalanceManager docBalanceManager;
        private TSP.DataManager.AccountingDocBalanceDetailManager docBalanceDetailManager;
        private TSP.DataManager.AccountingHistoryManager HistoryManager;
        private TSP.DataManager.AccountingHistoryDetailManager HistoryDetailManager;
        private TSP.DataManager.AccountingBankManager BankManager;
        private TSP.DataManager.AccountingProjectManager ProjectManager;
        private TSP.DataManager.AccountingCostCenterManager CCManager;
        private TSP.DataManager.AccountingDocBalanceManager DocBlcManager;
        private AccountingPriodManager AccountingPriodManager;
        
        #endregion

        #region Constructors
        public AccountingDocument(TSP.DataManager.TransactionManager transact, int AgentId, int ProjectId, int CCenterId)
        {
            docOpManager = new AccountingDocOperationManager();
            docOperationManager = new AccountingDocOperationManager();
            docBalanceManager = new AccountingDocBalanceManager();
            docBalanceDetailManager = new AccountingDocBalanceDetailManager();
            HistoryManager = new AccountingHistoryManager();
            HistoryDetailManager = new AccountingHistoryDetailManager();
            ProjectManager = new AccountingProjectManager();
            CCManager = new AccountingCostCenterManager();
            BankManager = new AccountingBankManager();
            DocBlcManager = new TSP.DataManager.AccountingDocBalanceManager();

            if (transact != null)
            {
                transact.Add(docOpManager);
                transact.Add(docOperationManager);
                transact.Add(docBalanceManager);
                transact.Add(docBalanceDetailManager);
                transact.Add(HistoryManager);
                transact.Add(HistoryDetailManager);
                transact.Add(BankManager);
                transact.Add(ProjectManager);
                transact.Add(CCManager);
                transact.Add(DocBlcManager);
            }

            AgentCode = AgentId;


            if (ProjectId == -1)
            {
                ProjectManager.Fill();
                if (ProjectManager.Count > 0)
                    PrjId = Convert.ToInt32(ProjectManager[0]["PrjId"]);
                else
                    PrjId = -1;
            }
            else
                PrjId = ProjectId;

            if (CCenterId == -1)
            {
                CCManager.Fill();
                if (CCManager.Count > 0)
                    CCId = Convert.ToInt32(CCManager[0]["CCId"]);
                else
                    CCId = -1;
            }
            else
                CCId = CCenterId;



            DateTime dt = new DateTime();
            dt = DateTime.Now;
            System.Globalization.PersianCalendar pDate = new System.Globalization.PersianCalendar();
            PerDate = string.Format("{0}/{1}/{2}", pDate.GetYear(dt).ToString().PadLeft(4, '0'), pDate.GetMonth(dt).ToString().PadLeft(2, '0'), pDate.GetDayOfMonth(dt).ToString().PadLeft(2, '0'));

        }

        public AccountingDocument(TSP.DataManager.TransactionManager transact, int UserId)//, int AgentId, int ProjectId, int CCenterId)
        {
            docOpManager = new AccountingDocOperationManager();
            docOperationManager = new AccountingDocOperationManager();
            docBalanceManager = new AccountingDocBalanceManager();
            docBalanceDetailManager = new AccountingDocBalanceDetailManager();
            HistoryManager = new AccountingHistoryManager();
            HistoryDetailManager = new AccountingHistoryDetailManager();
            ProjectManager = new AccountingProjectManager();
            CCManager = new AccountingCostCenterManager();
            BankManager = new AccountingBankManager();
            DocBlcManager = new TSP.DataManager.AccountingDocBalanceManager();
            AccountingPriodManager = new AccountingPriodManager();
            if (transact != null)
            {
                transact.Add(docOpManager);
                transact.Add(docOperationManager);
                transact.Add(docBalanceManager);
                transact.Add(docBalanceDetailManager);
                transact.Add(HistoryManager);
                transact.Add(HistoryDetailManager);
                transact.Add(BankManager);
                transact.Add(ProjectManager);
                transact.Add(CCManager);
                transact.Add(DocBlcManager);
            }
            UserCode = UserId;
            //AgentCode = AgentId;


            //if (ProjectId == -1)
            //{
            //    ProjectManager.Fill();
            //    if (ProjectManager.Count > 0)
            //        PrjId = Convert.ToInt32(ProjectManager[0]["PrjId"]);
            //    else
            //        PrjId = -1;
            //}
            //else
            //    PrjId = ProjectId;

            //if (CCenterId == -1)
            //{
            //    CCManager.Fill();
            //    if (CCManager.Count > 0)
            //        CCId = Convert.ToInt32(CCManager[0]["CCId"]);
            //    else
            //        CCId = -1;
            //}
            //else
            //    CCId = CCenterId;

            PerDate = Utility.GetDateOfToday();

        }

        #endregion

        #region Utility
        public string FindErrorMessage(int ErrorCode)
        {
            string Msg = "";
            switch (ErrorCode)
            {
                case (int)AccountingErrors.ErrorInSave:
                    Msg = "خطایی در ذخیره ایجاد شده است";
                    break;
                case (int)AccountingErrors.ErrorInFill:
                    Msg = "خطایی در بازیابی اطلاعات ایجاد شده است";
                    break;
                case (int)AccountingErrors.InvalidDocNumber:
                    Msg = "شماره سند وارد شده معتبر نمی باشد";
                    break;
            }
            return Msg;
        }

        public int GetNewDocNumber(int AgentId)
        {
            try
            {
                return (docBalanceManager.GetNewDocNumber(AgentId));
            }
            catch
            {
                return -1;
            }
        }
        #endregion
        /*********************************************** Insert ******************************************************/
        public int Insert(int AccIdBed, int AccIdBes, decimal Amount, string Description, int TableTypeCode, string BkDocNum, AccountingTT AccountingTTId, int UserId)
        {
            ActionMode = "New";
            TableTypeId = TableTypeCode;
            TTId = (int)AccountingTTId;
            Des = Description;
            BankDocNum = BkDocNum;
            UserCode = UserId;
            InsertDocOperation();
            InsertDocBalance();
            InsertDocBalanceDetail("Bed", Amount, AccIdBed);
            InsertDocBalanceDetail("Bes", Amount, AccIdBes);
            SaveHistory();
            return DocBalanceId;
        }

        private void InsertDocOperation()
        {
            if (docOperationManager.Count == 0)
            {
                DataRow rowDocOperation = docOperationManager.NewRow();

                rowDocOperation.BeginEdit();

                rowDocOperation["AgentId"] = AgentCode;
                rowDocOperation["DocNumber"] = GetMaxDocNumber();
                rowDocOperation["Description"] = Des;
                rowDocOperation["CreateDate"] = PerDate;
                rowDocOperation["DocStatusId"] = (int)DocumentStatusType.Movaghat;
                rowDocOperation["IsMerged"] = 0;
                rowDocOperation["IsBalanced"] = 1;
                rowDocOperation["InActive"] = 0;
                rowDocOperation["UserId"] = UserCode;
                rowDocOperation["ModifiedDate"] = DateTime.Now;
                rowDocOperation["TableTypeId"] = TableTypeId;

                rowDocOperation.EndEdit();

                docOperationManager.AddRow(rowDocOperation);
                docOperationManager.Save();
                docOperationManager.DataTable.AcceptChanges();
            }
            DocOperationId = Convert.ToInt32(docOperationManager[0]["DocOperationId"]);
            DocNumber = Convert.ToInt32(docOperationManager[0]["DocNumber"]);
        }

        private void InsertDocBalance()
        {
            if (docBalanceManager.Count == 0)
            {
                DataRow rowDocBalance = docBalanceManager.NewRow();

                rowDocBalance.BeginEdit();

                rowDocBalance["DocOperationId"] = DocOperationId;
                rowDocBalance["RegisterDate"] = PerDate;
                rowDocBalance["DocDate"] = PerDate;
                rowDocBalance["Description"] = Des;
                rowDocBalance["TTId"] = TTId;
                rowDocBalance["DirectAccess"] = 0;
                rowDocBalance["InActive"] = 0;
                rowDocBalance["UserId"] = UserCode;
                rowDocBalance["ModifiedDate"] = DateTime.Now;

                rowDocBalance.EndEdit();

                docBalanceManager.AddRow(rowDocBalance);
                docBalanceManager.Save();
                docBalanceManager.DataTable.AcceptChanges();
            }
            DocBalanceId = Convert.ToInt32(docBalanceManager[0]["DocBalanceId"]);
        }

        private void InsertDocBalanceDetail(string Flag, decimal Amount, int AccId)
        {
            DataRow rowDocBalanceDetail = docBalanceDetailManager.NewRow();

            rowDocBalanceDetail.BeginEdit();

            rowDocBalanceDetail["DocBalanceId"] = DocBalanceId;
            if (Flag == "Bes")
            {
                rowDocBalanceDetail["TotalBes"] = Amount;
                rowDocBalanceDetail["TotalBed"] = 0;
                rowDocBalanceDetail["AccId"] = AccId;
            }
            else if (Flag == "Bed")
            {
                rowDocBalanceDetail["TotalBes"] = 0;
                rowDocBalanceDetail["TotalBed"] = Amount;
                rowDocBalanceDetail["AccId"] = AccId;
            }
            //rowDocBalanceDetail["Description"] = Des;
            rowDocBalanceDetail["PrjId"] = PrjId;
            rowDocBalanceDetail["CCId"] = CCId;
            rowDocBalanceDetail["TTId"] = TTId;
            if (BankManager.IsBank(AccId) && !string.IsNullOrEmpty(BankDocNum))
                rowDocBalanceDetail["BankDocNum"] = BankDocNum;
            rowDocBalanceDetail["ModifiedDate"] = DateTime.Now;

            rowDocBalanceDetail.EndEdit();

            docBalanceDetailManager.AddRow(rowDocBalanceDetail);
            docBalanceDetailManager.Save();
            docBalanceDetailManager.DataTable.AcceptChanges();
        }

        /*********************************************** Insert And Add Details ***********************************************/
        public void AddDetails(string Flag, decimal Amount, int AccId, string Description)
        {
            DataRow rowDocBalanceDetail = docBalanceDetailManager.NewRow();

            rowDocBalanceDetail.BeginEdit();

            rowDocBalanceDetail["DocBalanceId"] = 0;
            if (Flag == "Bes")
            {
                rowDocBalanceDetail["TotalBes"] = Amount;
                rowDocBalanceDetail["TotalBed"] = 0;
                rowDocBalanceDetail["AccId"] = AccId;
            }
            else if (Flag == "Bed")
            {
                rowDocBalanceDetail["TotalBes"] = 0;
                rowDocBalanceDetail["TotalBed"] = Amount;
                rowDocBalanceDetail["AccId"] = AccId;
            }
            rowDocBalanceDetail["Description"] = Description;
            rowDocBalanceDetail["PrjId"] = PrjId;
            rowDocBalanceDetail["CCId"] = CCId;
            rowDocBalanceDetail["ModifiedDate"] = DateTime.Now;

            rowDocBalanceDetail.EndEdit();

            docBalanceDetailManager.AddRow(rowDocBalanceDetail);
        }

        public int Save(string Description, int TableTypeCode, string BkDocNum, AccountingTT AccountingTTId, int UserId)
        {
            ActionMode = "New";
            TableTypeId = TableTypeCode;
            TTId = (int)AccountingTTId;
            Des = Description;
            BankDocNum = BkDocNum;
            UserCode = UserId;

            InsertDocOperation();
            InsertDocBalance();
            for (int i = 0; i < docBalanceDetailManager.Count; i++)
            {
                docBalanceDetailManager[i].BeginEdit();

                docBalanceDetailManager[i]["DocBalanceId"] = DocBalanceId;
                docBalanceDetailManager[i]["TTId"] = TTId;
                if (BankManager.IsBank(Convert.ToInt32(docBalanceDetailManager[i]["AccId"])) && !string.IsNullOrEmpty(BankDocNum))
                    docBalanceDetailManager[i]["BankDocNum"] = BankDocNum;

                docBalanceDetailManager[i].EndEdit();
            }
            docBalanceDetailManager.Save();
            docBalanceDetailManager.DataTable.AcceptChanges();

            SaveHistory();
            return DocBalanceId;
        }

        /*********************************************** Update ******************************************************/
        public void Update(int DocOpId, int AccIdBed, int AccIdBes, decimal Amount, string Description, string BkDocNum)
        {
            ActionMode = "Edit";
            DocOperationId = DocOpId;
            GetdDocBalanceId();
            Des = Description;
            BankDocNum = BkDocNum;

            UpdateDocOperation();
            UpdateDocBalance();
            DeleteDocBalanceDetail();
            InsertDocBalanceDetail("Bed", Amount, AccIdBed);
            InsertDocBalanceDetail("Bes", Amount, AccIdBes);
            SaveHistory();
        }

        private void UpdateDocOperation()
        {
            docOperationManager.FindById(DocOperationId);

            if (docOperationManager.Count >= 1)
            {
                docOperationManager[0].BeginEdit();
                docOperationManager[0]["AgentId"] = AgentCode;
                //docOperationManager[0]["DocNumber"] = 
                //docOperationManager[0]["Description"] = 
                docOperationManager[0]["DocStatusId"] = (int)TSP.DataManager.DocumentStatusType.Movaghat;
                docOperationManager[0]["IsMerged"] = 0;
                docOperationManager[0]["IsBalanced"] = 1;
                docOperationManager[0]["InActive"] = 0;
                docOperationManager[0]["UserId"] = UserCode;
                docOperationManager[0]["ModifiedDate"] = DateTime.Now;
                docOperationManager[0].EndEdit();

                docOperationManager.Save();

                DocNumber = Convert.ToInt32(docOperationManager[0]["DocNumber"]);
            }
        }

        private void UpdateDocBalance()
        {
            docBalanceManager.FindById(DocBalanceId);
            if (docBalanceManager.Count >= 1)
            {
                DataRow rowDocBalance = docBalanceManager[0];

                rowDocBalance.BeginEdit();

                //rowDocBalance["DocOperationId"] = DocOperationId;
                //rowDocBalance["RegisterDate"] = PerDate;
                //rowDocBalance["DocDate"] = PerDate;
                //rowDocBalance["Description"] = "";
                rowDocBalance["TTId"] = TTId;
                rowDocBalance["DirectAccess"] = 0;
                rowDocBalance["InActive"] = 0;
                rowDocBalance["UserId"] = UserCode;
                rowDocBalance["ModifiedDate"] = DateTime.Now;

                rowDocBalance.EndEdit();

                docBalanceManager.Save();
            }
        }

        private void DeleteDocBalanceDetail()
        {
            docBalanceDetailManager.FindByParentId(DocBalanceId);
            int Count = docBalanceDetailManager.Count;
            for (int i = 0; i < Count; i++)
                docBalanceDetailManager[0].Delete();
        }

        #region History
        public int SaveHistory(int DocBalanceId, int ActionId, string Decription)
        {
            InsertHistory(DocBalanceId, ActionId, Decription);
            if (ActionId == (int)AccountingAction.New
                || ActionId == (int)AccountingAction.Edit)//*****Submit,Reverse=New,Edit
            {
                docBalanceDetailManager.FindByParentId(DocBalanceId);
                InsertHistoryDetail();
            }
            return (int)AccountingErrors.Succed;
        }

        private void InsertHistory(int DocBalanceId, int ActionId, string Decription)
        {
            //  string HId;

            DataRow HistoryRow = HistoryManager.NewRow();

            HistoryRow["DocBalanceId"] = DocBalanceId;
            HistoryRow["ActionId"] = ActionId;
            HistoryRow["Description"] = Decription;
            HistoryRow["CreateDate"] = Utility.GetDateOfToday();
            HistoryRow["TotalBes"] = GetTotalBes();
            HistoryRow["TotalBed"] = GetTotalBed();
            HistoryRow["UserId"] = UserCode;
            HistoryRow["ModifiedDate"] = DateTime.Now;

            HistoryManager.AddRow(HistoryRow);

            HistoryManager.Save();
            HistoryManager.DataTable.AcceptChanges();

            HId = (int)HistoryManager[HistoryManager.Count - 1]["HId"];
        }

        private decimal GetTotalBes()
        {
            decimal TotalBes = 0;

            docBalanceManager.FindByParentId(DocOperationId);
            if (docBalanceManager.Count > 0)
            {
                docBalanceDetailManager.FindByParentId(Convert.ToInt32(docBalanceManager[0]["DocBalanceId"]));
                for (int i = 0; i < docBalanceDetailManager.Count; i++)
                    TotalBes = TotalBes + Convert.ToDecimal(docBalanceDetailManager[i]["TotalBes"]);
            }

            return TotalBes;
        }

        private decimal GetTotalBed()
        {
            decimal TotalBed = 0;

            docBalanceManager.FindByParentId(DocOperationId);
            if (docBalanceManager.Count > 0)
            {
                docBalanceDetailManager.FindByParentId(Convert.ToInt32(docBalanceManager[0]["DocBalanceId"]));
                for (int i = 0; i < docBalanceDetailManager.Count; i++)
                    TotalBed = TotalBed + Convert.ToDecimal(docBalanceDetailManager[i]["TotalBed"]);
            }

            return TotalBed;
        }

        private void InsertHistoryDetail()
        {
            for (int i = 0; i < docBalanceDetailManager.Count; i++)
            {
                DataRow HistoryDetailRow = HistoryDetailManager.NewRow();

                HistoryDetailRow["HId"] = HId;
                HistoryDetailRow["DocBalanceDetailId"] = docBalanceDetailManager[i]["DocBalanceDetailId"];
                HistoryDetailRow["TotalBes"] = docBalanceDetailManager[i]["TotalBes"];
                HistoryDetailRow["TotalBed"] = docBalanceDetailManager[i]["TotalBed"];
                HistoryDetailRow["AccId"] = docBalanceDetailManager[i]["AccId"];
                HistoryDetailRow["PrjId"] = docBalanceDetailManager[i]["PrjId"];
                HistoryDetailRow["CCId"] = docBalanceDetailManager[i]["CCId"];
                HistoryDetailRow["Description"] = docBalanceDetailManager[i]["Description"];
                HistoryDetailRow["Comment"] = docBalanceDetailManager[i]["Comment"];
                HistoryDetailRow["ModifiedDate"] = DateTime.Now;

                HistoryDetailManager.AddRow(HistoryDetailRow);
            }
            HistoryDetailManager.Save();
            HistoryDetailManager.DataTable.AcceptChanges();
        }
        /*********************************************** History *****************************************************/
        private int GetAction()
        {
            switch (ActionMode)
            {
                case "New":
                    return (int)TSP.DataManager.AccountingAction.New;

                case "View":
                    return (int)TSP.DataManager.AccountingAction.View;

                case "Edit":
                    return (int)TSP.DataManager.AccountingAction.Edit;

                default:
                    return -1;
            }
        }

        private void InsertHistory()
        {
            DataRow HistoryRow = HistoryManager.NewRow();

            HistoryRow["DocOperationId"] = DocOperationId;
            HistoryRow["ActionId"] = GetAction();
            //HistoryRow["Description"] = ;
            HistoryRow["CreateDate"] = PerDate;
            HistoryRow["TotalBes"] = GetTotalBes();
            HistoryRow["TotalBed"] = GetTotalBed();
            HistoryRow["UserId"] = UserCode;
            HistoryRow["ModifiedDate"] = DateTime.Now;

            HistoryManager.AddRow(HistoryRow);

            HistoryManager.Save();
            HistoryManager.DataTable.AcceptChanges();

            HId = Convert.ToInt32(HistoryManager[0]["HId"]);
        }

        private void SaveHistory()
        {
            InsertHistory();

            if (ActionMode == "New" || ActionMode == "Edit")
            {
                docBalanceDetailManager.FindByParentId(DocBalanceId);
                InsertHistoryDetail();
            }
        }
        /*************************************************************************************************************/

        #endregion

        private int GetMaxDocNumber()
        {
            return docOpManager.GetLastDocNumber(AgentCode);
        }

        public void GetdDocBalanceId()
        {
            DocBlcManager.FindByParentId(DocOperationId);
            if (DocBlcManager.Count == 1)
                DocBalanceId = Convert.ToInt32(DocBlcManager[0]["DocBalanceId"]);
            else
                DocBalanceId = -1;
        }


        /***************************************************************************************************************/
        public int GetDocNumber()
        {
            return DocNumber;
        }

        #region CopyDocument
        public int CopyDocument(int docBalanceId, Boolean CopyDescription, Boolean ReverseDetails)
        {
            #region InitParametersForCopyDoc
            docBalanceManager.FindById(docBalanceId);
            if (docBalanceManager.Count != 1)
            {
                return (int)AccountingErrors.ErrorInFill;
            }
            AgentCode = Convert.ToInt32(docBalanceManager[0]["AgentId"]);
            if (CopyDescription)
                Des = docBalanceManager[0]["Description"].ToString();
            else
                Des = "";
            DocNumber = GetNewDocNumber(AgentCode);
            if (DocNumber < 0)
                return (int)AccountingErrors.ErrorInSave;
            RefName = docBalanceManager[0]["RefName"].ToString();
            DirectAccess = 1;
            TTId = (int)TSP.DataManager.AccountingTT.Dasti;
            ImageURL = docBalanceManager[0]["ImageURL"].ToString();
            DocStatusId = (int)DocumentStatusType.PishNevis;
            IsBalanced = 1;
            IsMerged = 0;
            #endregion

            if (InsertDocOprationInfo() != (int)AccountingErrors.Succed)
                return (int)AccountingErrors.ErrorInSave;
            if (InsertDocBalanceInfo() != (int)AccountingErrors.Succed)
                return (int)AccountingErrors.ErrorInSave;
            docBalanceDetailManager.FindByParentId(docBalanceId);
            int cntDetails = docBalanceDetailManager.Count;
            for (int i = 0; i < cntDetails; i++)
            {
                DataRow rowDocBalanceDetail = docBalanceDetailManager.NewRow();

                rowDocBalanceDetail["AccId"] = docBalanceDetailManager[i]["AccId"];
                rowDocBalanceDetail["PrjId"] = docBalanceDetailManager[i]["PrjId"];
                rowDocBalanceDetail["CCId"] = docBalanceDetailManager[i]["CCId"];
                if (ReverseDetails)
                {
                    string TotalBes = docBalanceDetailManager[i]["TotalBes"].ToString();
                    rowDocBalanceDetail["TotalBes"] = docBalanceDetailManager[i]["TotalBed"];
                    rowDocBalanceDetail["TotalBed"] = TotalBes;
                }
                else
                {
                    rowDocBalanceDetail["TotalBes"] = docBalanceDetailManager[i]["TotalBes"];
                    rowDocBalanceDetail["TotalBed"] = docBalanceDetailManager[i]["TotalBed"];

                }
                rowDocBalanceDetail["DocBalanceId"] = DocBalanceId;
                rowDocBalanceDetail["Description"] = docBalanceDetailManager[i]["Description"];
                rowDocBalanceDetail["Comment"] = docBalanceDetailManager[i]["Comment"];
                rowDocBalanceDetail["AgentId"] = docBalanceDetailManager[i]["AgentId"];
                rowDocBalanceDetail["TableTypeId"] = docBalanceDetailManager[i]["TableTypeId"];
                rowDocBalanceDetail["TableId"] = docBalanceDetailManager[i]["TableId"];
                rowDocBalanceDetail["BankDocNum"] = docBalanceDetailManager[i]["BankDocNum"];
                rowDocBalanceDetail["CoId"] = docBalanceDetailManager[i]["CoId"];
                rowDocBalanceDetail["TTId"] = docBalanceDetailManager[i]["TTId"];

                rowDocBalanceDetail["ModifiedDate"] = DateTime.Now;

                docBalanceDetailManager.AddRow(rowDocBalanceDetail);
            }
            if (cntDetails > 0 && docBalanceDetailManager.Save() <= 0)
            {
                return (int)AccountingErrors.ErrorInSave;
            }
            return (int)AccountingErrors.Succed;
        }

        private int InsertDocOprationInfo()
        {
            DataRow rowDocOperation = docOperationManager.NewRow();

            rowDocOperation["AgentId"] = AgentCode;
            rowDocOperation["DocNumber"] = DocNumber;
            rowDocOperation["Description"] = Des;
            rowDocOperation["CreateDate"] = PerDate;
            rowDocOperation["DocStatusId"] = DocStatusId;
            rowDocOperation["IsMerged"] = IsMerged;
            rowDocOperation["IsBalanced"] = IsBalanced;
            rowDocOperation["InActive"] = 0;
            rowDocOperation["UserId"] = UserCode;
            rowDocOperation["ModifiedDate"] = DateTime.Now;
            rowDocOperation["TableTypeId"] = TableTypeId;

            docOperationManager.AddRow(rowDocOperation);
            docOperationManager.Save();
            docOperationManager.DataTable.AcceptChanges();

            DocOperationId = Convert.ToInt32(docOperationManager[0]["DocOperationId"]);
            DocNumber = Convert.ToInt32(docOperationManager[docOperationManager.Count - 1]["DocNumber"]);
            return (int)AccountingErrors.Succed;
        }

        private int InsertDocBalanceInfo()
        {
            DataRow rowDocBalance = docBalanceManager.NewRow();

            rowDocBalance["DocOperationId"] = DocOperationId;
            rowDocBalance["RegisterDate"] = PerDate;
            rowDocBalance["DocDate"] = PerDate;
            rowDocBalance["Description"] = Des;
            rowDocBalance["TTId"] = TTId;
            rowDocBalance["DirectAccess"] = DirectAccess;
            rowDocBalance["InActive"] = 0;

            rowDocBalance["AgentId"] = AgentCode;
            rowDocBalance["DocNumber"] = DocNumber;
            rowDocBalance["DocStatusId"] = DocStatusId;
            rowDocBalance["RefName"] = RefName;

            rowDocBalance["UserId"] = UserCode;
            rowDocBalance["ModifiedDate"] = DateTime.Now;

            docBalanceManager.AddRow(rowDocBalance);
            docBalanceManager.Save();
            docBalanceManager.DataTable.AcceptChanges();

            DocBalanceId = Convert.ToInt32(docBalanceManager[docBalanceManager.Count - 1]["DocBalanceId"]);

            return (int)AccountingErrors.Succed;
        }

        private int InsetDocBalanceDetailInfo()
        {
            return (int)AccountingErrors.Succed;
        }
        #endregion

        #region Replace Document
        //public int CheckConditionForReplaceDoc(int SourceDocBalanceId, int TargetDocNumber)
        //{
        //    TSP.DataManager.AccountingDocBalanceManager AccountingDocBalanceManager = new TSP.DataManager.AccountingDocBalanceManager();
        //    AccountingDocBalanceManager.FindById(SourceDocBalanceId);
        //    if (AccountingDocBalanceManager.Count != 1)
        //    {
        //        //SetReplaceDocMessage("خطایی در بازیابی اطلاعات بوجود آمده است", System.Drawing.Color.Red);
        //        return (int)AccountingErrors.ErrorInFill;
        //    }
        //    int SourceDocNumber = Convert.ToInt32(AccountingDocBalanceManager[0]["DocNumber"]);
        //    string SourceDocDate = AccountingDocBalanceManager[0]["DocDate"].ToString();
        //    //TSP.DataManager.AccountingDocOperationManager AccountingDocOperationManager = new TSP.DataManager.AccountingDocOperationManager();
        //    int AgentId = -1;
        //    AccountingDocBalanceManager.FindActiveDocByDocNumber(AgentId, TargetDocNumber);
        //    if (AccountingDocBalanceManager.Count != 1)
        //    {
        //       // SetReplaceInfoMessage("شماره سند وارد شدده معتبر نمی باشد", 0);
        //        return (int)AccountingErrors.InvalidDocNumber;
        //    }
        //    int TargetDocBalanceId = Convert.ToInt32(AccountingDocBalanceManager[0]["DocNumber"]);
        //    string TargetDocDate = AccountingDocBalanceManager[0]["DocDate"].ToString();

        //}

        public int RelocationDocument(int AgentId, int SourceDocBalenceId, int TargetDocNo, int RelocationType)
        {
            //int SourceDocBalenceId = -1;
            int FromDocNo = -1;
            int ToDocNo = -1;
            string Operator = "+";

            int SourceDocNo = -1;
            string SourceDocDate = "";
            string TargetDocDate = "";

            docBalanceManager.FindById(SourceDocBalenceId);
            if (docBalanceManager.Count != 1)
                return (int)AccountingErrors.ErrorInFill;
            SourceDocDate = docBalanceManager[0]["DocDate"].ToString();
            SourceDocNo = Convert.ToInt32(docBalanceManager[0]["DocNumber"]);

            docBalanceManager.FindActiveDocByDocNumber(AgentId, TargetDocNo);
            if (docBalanceManager.Count != 1)
                return (int)AccountingErrors.InvalidDocNumber;
            TargetDocDate = docBalanceManager[0]["DocDate"].ToString();

            if (SourceDocNo >= TargetDocNo)//****کشیدن سند به عقب
            {
                Operator = "+";
                switch (RelocationType)
                {
                    case (int)AccountingDocumentRelocationType.InsteadOf:
                        FromDocNo = TargetDocNo + 1;
                        ToDocNo = SourceDocNo - 1;
                        break;
                    case (int)AccountingDocumentRelocationType.After:
                        FromDocNo = TargetDocNo + 2;
                        ToDocNo = SourceDocNo - 1;
                        break;
                }
            }
            else if (SourceDocNo < TargetDocNo)//****کشیدن سند به جلو
            {
                Operator = "-";
                switch (RelocationType)
                {
                    case (int)AccountingDocumentRelocationType.InsteadOf:
                        FromDocNo = SourceDocNo + 1;
                        ToDocNo = TargetDocNo;
                        break;
                    case (int)AccountingDocumentRelocationType.After:
                        FromDocNo = SourceDocNo + 1;
                        ToDocNo = TargetDocNo + 1;

                        break;
                }
            }
            docBalanceManager.FindActiveDocByDocNumber(AgentId, FromDocNo, ToDocNo);
            for (int i = 0; i < docBalanceManager.Count; i++)
            {
                docBalanceManager[i].BeginEdit();
                int TempDocNumber = Convert.ToInt32(docBalanceManager[i]["DocNumber"]);
                if (Operator == "+")
                    TempDocNumber++;
                if (Operator == "-")
                    TempDocNumber--;
                docBalanceManager[i]["DocNumber"] = TempDocNumber;
                docBalanceManager[i].EndEdit();
                docBalanceManager.Save();
                docBalanceManager.DataTable.AcceptChanges();
            }

            docBalanceManager.FindById(SourceDocBalenceId);
            if (docBalanceManager.Count != 1)
                return (int)AccountingErrors.ErrorInSave;
            docBalanceManager[0].BeginEdit();
            docBalanceManager[0]["DocNumber"] = TargetDocNo;
            docBalanceManager[0]["DocDate"] = TargetDocDate;

            docBalanceManager[0].EndEdit();
            docBalanceManager.Save();

            return (int)AccountingErrors.Succed;
        }
        #endregion

        public int InActiveAccountingDocument(int DocBalanceId)
        {
            docBalanceManager.FindById(DocBalanceId);
            if (docBalanceManager.Count != 1)
                return (int)AccountingErrors.ErrorInSave;
            int AgentId = Convert.ToInt32(docBalanceManager[0]["AgentId"]);
            docBalanceManager[0].BeginEdit();
            docBalanceManager[0]["Inactive"] = 1;
            docBalanceManager[0]["UserId"] = UserCode;
            docBalanceManager[0]["ModifiedDate"] = DateTime.Now;
            docBalanceManager[0].EndEdit();
            if (docBalanceManager.Save() <= 0)
                return (int)AccountingErrors.ErrorInSave;
            SaveHistory(DocBalanceId, (int)AccountingAction.Inactive, "");
            int PriodId = AccountingPriodManager.SelectPeriodByDate(docBalanceManager[0]["DocDate"].ToString());         
            docBalanceManager.UpdateDocumentNumberByDate(AgentId,PriodId);

            return (int)AccountingErrors.Succed;
        }
    }
}
