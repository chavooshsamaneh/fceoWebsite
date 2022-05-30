using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Utility
{
    /// <summary>
    /// Summary description for Login
    /// </summary>
    public class Login
    {
        private String Username;
        private String Password;

        private int _UserId;
        public int UserId
        {
            get { return _UserId; }
        }

        private Boolean _IsTspAdmin;
        public Boolean IsTspAdmin
        {
            get { return _IsTspAdmin; }
        }

        private Boolean _NeedTempPass;
        public Boolean NeedTempPass
        {
            get { return _NeedTempPass; }
        }
        public Login(String Username, String Password)
        {
            this.Username = Username;
            this.Password = Password;
            _IsTspAdmin = false;
            _NeedTempPass = false;
        }

        public Boolean CheckLogin()
        {
            try
            {
                _IsTspAdmin = false;
                _NeedTempPass = false;
                String[] AdminConfirm = GetAdminConfirm().Split('$');
                String[] Username = this.Username.Split(':');
                String[] AdminConfirmUrgent = GetUrgenAdminConfirm().Split('$');
                DataTable LoginData;
                if (Username.Length == 0) return false;
                TSP.DataManager.LoginManager LoginManage = new TSP.DataManager.LoginManager();

                if (EncryptPassword2(Username[0].ToLower()) == AdminConfirm[0])
                {
                    if (EncryptPassword2(this.Password) == AdminConfirm[1])
                    {
                        if (Username.Length == 1)
                        {
                            _UserId = 1;
                            _IsTspAdmin = true;
                            return true;
                        }                      

                    }

                }

                LoginData = LoginManage.LoginUser(Username[0], EncryptPassword(this.Password));
                if (LoginData.Rows.Count > 0)
                {
                    _UserId = int.Parse(LoginData.Rows[0]["UserId"].ToString());
                    _NeedTempPass = Convert.ToBoolean(LoginData.Rows[0]["NeedTempPass"]);
                    return true;
                }
            }
            catch (Exception) { }
            return false;
        }
    }
}