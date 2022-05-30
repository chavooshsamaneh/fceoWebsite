using System;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager
{
    public class TransactionManager : IDisposable
    {
        #region Properties
        internal System.Data.SqlClient.SqlTransaction Transaction
        {
            get
            {
                return sqlTransaction;
            }
        }
        #endregion

        #region Members
        private System.Collections.ArrayList objects;
        private System.Data.SqlClient.SqlConnection sqlConnection;
        private System.Data.SqlClient.SqlTransaction sqlTransaction;
        #endregion

        #region Constructors
        public TransactionManager(System.Data.SqlClient.SqlConnection Connection)
        {
            sqlConnection = Connection;
            objects = new System.Collections.ArrayList();
        }

        public TransactionManager()
        {
            sqlConnection = new System.Data.SqlClient.SqlConnection(DBManager.CnnStr);
            objects = new System.Collections.ArrayList();
        }
        #endregion      

        public void Add(BaseObject o)
        {
            //	try
            {
                objects.Add(o);
                o.Connection = sqlConnection;
                o.Transaction = sqlTransaction;
            }
            //	catch(Exception err)
            {
                //	System.Windows.Forms.MessageBox.Show(err.Message+err.StackTrace);
            }
        }

        public void Add(BaseObject[] o)
        {
            for (int i = 0; i < o.Length; i++)
            {
                Add(o[i]);
                //((IAccBusiness)o[i]).Connection=sqlConnection;
            }
        }

        public void RemoveAll()
        {
            objects.Clear();
        }

        public void BeginSave()
        {
            // sqlConnection.FireInfoMessageEventOnUserErrors = true;
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
                sqlConnection.Open();
            sqlTransaction = sqlConnection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

            for (int i = 0; i < objects.Count; i++)
            {
                ((BaseObject)objects[i]).Transaction = sqlTransaction;
            }
        }

        public void EndSave()
        {
            //for (int i = 0; i < objects.Count; i++)
            //{
            //    ((BaseObject)objects[i]).Save();
            //}            
            sqlTransaction.Commit();

            for (int i = 0; i < objects.Count; i++)
            {
                ((BaseObject)objects[i]).AcceptChanges();
                ((BaseObject)objects[i]).Transaction = null;
            }
            //this.sqlConnection.Close();
            //this.sqlConnection=null;
            sqlTransaction = null;
            //objects=null;
        }

        public void CancelSave()
        {
            if (sqlTransaction != null)
                sqlTransaction.Rollback();
            ((BaseObject)objects[0]).Connection.Close();
            for (int i = 0; i < objects.Count; i++)
            {
                if (((BaseObject)objects[i]).DataSet.Tables.Count > 0 && ((BaseObject)objects[i]).DataSet.Tables[0].ChildRelations.Count > 0)
                {
                    if (i == 0)
                        ((BaseObject)objects[i]).ResetChanges();
                }
                else
                    ((BaseObject)objects[i]).ResetChanges();
                for (int j = 0; j < ((BaseObject)objects[i]).DataSet.Tables.Count; j++)
                {
                    System.Data.DataRow[] dr = ((BaseObject)objects[i]).DataSet.Tables[j].GetErrors();
                    for (int k = 0; k < dr.Length; k++)
                    {
                        dr[k].ClearErrors();
                        dr[k].EndEdit();
                    }
                }
                ((BaseObject)objects[i]).Transaction = null;
            }
        }

        public void Dispose()
        {


        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            this.sqlConnection.Dispose();
            this.sqlTransaction.Dispose();

            this.sqlConnection = null;
            this.sqlTransaction = null;

        }

        #endregion
    }
}
