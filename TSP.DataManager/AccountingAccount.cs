using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace TSP.DataManager
{
    public enum AccUserControlCallbackType
    {
        Search,
        NodeClick
    }

    public class AccountingAccount
    {


        private int AgentCode, UserCode;

        private TSP.DataManager.AccountingAccountManager AccountManager;
        private TSP.DataManager.AccountingAccountManager ParentAccountManager;

        public AccountingAccount(TSP.DataManager.TransactionManager transact, int AgentId)
        {
            AccountManager = new AccountingAccountManager();
            ParentAccountManager = new AccountingAccountManager();

            if (transact != null)
            {
                transact.Add(AccountManager);
            }

            AgentCode = AgentId;
            
        }

        /*********************************************** Insert ******************************************************/
        public int InsertAccount(int ParentAccId, string AccCode, string AccName, int UserId)
        {
            ParentAccountManager.FindByAccId(ParentAccId);
            if (ParentAccountManager.Count > 0)
            {
                //AccCode = ParentAccountManager[0]["AccCode"].ToString() + "-" + AccCode;

                DataRow rowAccount = AccountManager.NewRow();

                rowAccount.BeginEdit();

                rowAccount["AccCode"] = ParentAccountManager[0]["AccCode"].ToString() + "-" + AccCode;
                rowAccount["AccName"] = AccName;
                rowAccount["AccTypeId"] = (int)AccountingAccType.Tafzili;
                rowAccount["AccGrpId"] = ParentAccountManager[0]["AccGrpId"];
                //rowAccount["AccDescription"] = txtDescription.Text;
                rowAccount["FirstInvoice"] = 0;
                rowAccount["ParentId"] = ParentAccId;
                rowAccount["AgentId"] = AgentCode;
                rowAccount["K"] = ParentAccountManager[0]["K"];
                rowAccount["M"] = ParentAccountManager[0]["M"];
                rowAccount["T"] = AccCode;
                rowAccount["Inactive"] = 0;
                rowAccount["UserId"] = UserId;
                rowAccount["ModifiedDate"] = DateTime.Now;

                rowAccount.EndEdit();

                AccountManager.AddRow(rowAccount);

                AccountManager.Save();

                AccountManager.AcceptChanges();
                return Convert.ToInt32(AccountManager[0]["AccId"]);
            }
            return -1;
        }
    }
}
