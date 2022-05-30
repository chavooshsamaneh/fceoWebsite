using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class LoginPass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //  TSP.DataManager.TeacherManager TeacherManager = new TSP.DataManager.TeacherManager();
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        TSP.DataManager.LoginManager LoginManager1 = new TSP.DataManager.LoginManager();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        TransactionManager.Add(LoginManager);
        TransactionManager.Add(MemberManager);
        //  TransactionManager.Add(TeacherManager);

        try
        {
            string IdNo = "";
            string LastMeId = "";
            string Error = "Finished";
            TransactionManager.BeginSave();
            LoginManager1.FindByMeIdUltId(-1, (int)TSP.DataManager.UserType.Member);
            DataRow[] drLog = LoginManager1.DataTable.Select("MeId in ()");//("MeId>=14371 and MeId<=15060");
            for (int i = 0; i < drLog.Length; i++)
            {
                int LoginId = Convert.ToInt32(drLog[i]["UserId"]);
                MemberManager.FindByCode(Convert.ToInt32(drLog[i]["MeId"]));
                LastMeId = drLog[i]["MeId"].ToString();
                if (MemberManager.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(MemberManager[0]["IdNo"]))// && !string.IsNullOrEmpty(MemberManager[0]["IdNo"].ToString()))
                    {
                        if (LastMeId == "13432" || LastMeId == "15060")
                            IdNo = "11111";
                        else
                            IdNo = MemberManager[0]["IdNo"].ToString();
                        LoginManager.FindByCode(LoginId);
                        if (LoginManager.Count == 1)
                        {
                            LoginManager[0].BeginEdit();
                            LoginManager[0]["Password"] = FormsAuthentication.HashPasswordForStoringInConfigFile(IdNo.ToString(), "sha1");
                            LoginManager[0].EndEdit();
                            LoginManager.Save();
                            LoginManager.DataTable.AcceptChanges();
                        }
                    }
                    else
                        Error += " " + drLog[i]["MeId"].ToString();
                    lblMsg.Text = Error;
                }
            }
            TransactionManager.EndSave();
            lblMsg.Text = Error;
        }
        catch
        {
            TransactionManager.CancelSave();
            lblMsg.Text = "Error";
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        //TSP.DataManager.LoginManager LoginManager1 = new TSP.DataManager.LoginManager();
        TSP.DataManager.EmployeeManager EmployeeManager = new TSP.DataManager.EmployeeManager();
        TransactionManager.Add(LoginManager);
        TransactionManager.Add(EmployeeManager);
        //  TransactionManager.Add(TeacherManager);

        try
        {
            string SSN = "";
            string LastEmpId = "";
            string Error = "Finished";
            TransactionManager.BeginSave();
            LoginManager.FindByMeIdUltId(-1, (int)TSP.DataManager.UserType.Employee);
            for (int i = 0; i < LoginManager.Count; i++)
            {
                int LoginId = Convert.ToInt32(LoginManager[i]["UserId"]);
                EmployeeManager.FindByCode(Convert.ToInt32(LoginManager[i]["MeId"]));
                LastEmpId = LoginManager[i]["MeId"].ToString();
                if (EmployeeManager.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(EmployeeManager[0]["SSN"]))// && !string.IsNullOrEmpty(MemberManager[0]["IdNo"].ToString()))
                    {
                        //1,9,11,17,19,30,47
                        if (LastEmpId != "1" && LastEmpId != "9" && LastEmpId != "11" && LastEmpId != "17" && LastEmpId != "19"
                            && LastEmpId != "30" && LastEmpId != "47")
                        {
                            SSN = EmployeeManager[0]["SSN"].ToString();
                            LoginManager[i].BeginEdit();
                            LoginManager[i]["Password"] = FormsAuthentication.HashPasswordForStoringInConfigFile(SSN.ToString(), "sha1");
                            LoginManager[i].EndEdit();
                            LoginManager.Save();
                            LoginManager.DataTable.AcceptChanges();
                        }
                    }
                    else
                        Error += " " + LoginManager[i]["MeId"].ToString();
                    lblMsg.Text = Error;
                }
            }
            TransactionManager.EndSave();
            lblMsg.Text = Error;
        }
        catch
        {
            TransactionManager.CancelSave();
            lblMsg.Text = "Error";
        }
    }

    protected void btnOfficePass_Click(object sender, EventArgs e)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.LoginManager LoginManager = new TSP.DataManager.LoginManager();
        //TSP.DataManager.LoginManager LoginManager1 = new TSP.DataManager.LoginManager();
        TSP.DataManager.OfficeManager OfficeManager = new TSP.DataManager.OfficeManager();
        TSP.DataManager.OfficeMemberManager OfficeMemberManager = new TSP.DataManager.OfficeMemberManager();
        TransactionManager.Add(LoginManager);
        TransactionManager.Add(OfficeManager);
        TransactionManager.Add(OfficeMemberManager);
        //  TransactionManager.Add(TeacherManager);

        try
        {
            string ManagerOfmId = "";
            string ManagerMeId = "";
            string LastOfId = "";
            string Error = "Finished";
            TransactionManager.BeginSave();
            LoginManager.FindByMeIdUltId(-1, (int)TSP.DataManager.UserType.Office);
            for (int i = 0; i < LoginManager.Count; i++)
            {
                int LoginId = Convert.ToInt32(LoginManager[i]["UserId"]);
                OfficeManager.FindByCode(Convert.ToInt32(LoginManager[i]["MeId"]));
                LastOfId = LoginManager[i]["MeId"].ToString();
                if (OfficeManager.Count == 1)
                {
                    if (!Utility.IsDBNullOrNullValue(OfficeManager[0]["ManagerOfmId"]))// && !string.IsNullOrEmpty(MemberManager[0]["IdNo"].ToString()))
                    {
                        OfficeMemberManager.FindByCode(Convert.ToInt32(OfficeManager[0]["ManagerOfmId"]));
                        if (OfficeMemberManager.Count == 1)
                        {
                            ManagerMeId = OfficeMemberManager[0]["PersonId"].ToString();
                            //1,9,11,17,19,30,47
                            //if (LastOfId != "1" && LastOfId != "9" && LastOfId != "11" && LastOfId != "17" && LastOfId != "19"
                            //    && LastOfId != "30" && LastOfId != "47")
                            //{
                           // ManagerOfmId = OfficeManager[0]["ManagerOfmId"].ToString();
                            LoginManager[i].BeginEdit();
                            LoginManager[i]["Password"] = FormsAuthentication.HashPasswordForStoringInConfigFile(ManagerMeId.ToString(), "sha1");
                            LoginManager[i].EndEdit();
                            LoginManager.Save();
                            LoginManager.DataTable.AcceptChanges();
                        }
                        //}
                    }
                    else
                        Error += " " + LoginManager[i]["MeId"].ToString();
                    lblMsg.Text = Error;
                }
            }
            TransactionManager.EndSave();
            lblMsg.Text = Error;
        }
        catch
        {
            TransactionManager.CancelSave();
            lblMsg.Text = "Error";
        }
    }
}
