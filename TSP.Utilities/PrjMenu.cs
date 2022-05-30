using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// توابع مربوط به ساب منوی پروژه
/// </summary>
/// 
public class PrjMenu
{
    public class ProjectMenusViewPermission
    {
        public bool CanViewBaseInfo;
        public bool CanViewRegisteredNo;
        public bool CanViewPlansMethod;
        public bool CanViewBlock;
        public bool CanViewInsurance;
    }

    private string Title, CurrentTitle;
    int TitleNo, CurrentTitleNo, ProjectId;

    private enum MenuTitle
    {
        BaseInfo = 1,
        RegisteredNo = 2,
        PlansMethod = 3,
        Block = 4,
        Insurance = 5
    }

    public PrjMenu(string CurrentMenuTitle, int PrjId)
    {
        CurrentTitle = CurrentMenuTitle;
        CurrentTitleNo = GetTitleNos(CurrentTitle);
        ProjectId = PrjId;
    }

    private int GetTitleNos(string Name)
    {
        switch (Name)
        {
            case "BaseInfo":
                return 1;
                break;

            case "RegisteredNo":
                return 2;
                break;

            case "PlansMethod":
                return 3;
                break;

            case "Block":
                return 4;
                break;

            case "Insurance":
                return 5;
                break;

            default:
                return 0;
        }
    }

    private bool CheckFormula()
    {
        if (TitleNo <= CurrentTitleNo)
            return true;
        else if (CheckPervExistance() == true)
            return true;
        else
            return CheckExistance();
    }

    private bool CheckPervExistance()
    {
        switch (Title)
        {
            case "RegisteredNo":
                return CheckProject();
                break;

            case "PlansMethod":
                return CheckRegisteredNo();
                break;

            case "Block":
                return CheckPlansMethod();
                break;

            case "Insurance":
                return CheckBlock();
                break;

            default:
                return false;
        }
    }

    private bool CheckExistance()
    {
        switch (Title)
        {
            case "BaseInfo":
                return true;
                break;

            case "RegisteredNo":
                return CheckRegisteredNo();
                break;

            case "PlansMethod":
                return CheckPlansMethod();
                break;

            case "Block":
                return CheckBlock();
                break;

            case "Insurance":
                return CheckInsurance();
                break;

            default:
                return false;
        }
    }

    private bool CheckProject()
    {
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
        ProjectManager.FindByProjectId(ProjectId);
        if (ProjectManager.Count == 1)
            return true;
        else
            return false;
    }

    private bool CheckRegisteredNo()
    {
        TSP.DataManager.TechnicalServices.RegisteredNoManager RegisteredNoManager = new TSP.DataManager.TechnicalServices.RegisteredNoManager();
        RegisteredNoManager.FindByProjectId(ProjectId);
        if (RegisteredNoManager.Count > 0)
            return true;
        else
            return false;
    }

    private bool CheckPlansMethod()
    {
        return true;
        //**********اجباری بودن دستور نقشه حذف شد**************
        TSP.DataManager.TechnicalServices.PlansMethodManager PlansMethodManager = new TSP.DataManager.TechnicalServices.PlansMethodManager();
        PlansMethodManager.FindByProjectId(ProjectId);
        if (PlansMethodManager.Count > 0)
            return true;
        else
            return false;
    }

    private bool CheckBlock()
    {
        TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();       
        if (BlockManager.SelectTSBlockCountByProject(ProjectId) > 0)
            return true;
        else
            return false;
    }

    private bool CheckInsurance()
    {
        TSP.DataManager.TechnicalServices.InsuranceManager InsuranceManager = new TSP.DataManager.TechnicalServices.InsuranceManager();
        InsuranceManager.FindByProjectId(ProjectId);
        if (InsuranceManager.Count > 0)
            return true;
        else
            return false;
    }

    /****************************************************** Public Methods ******************************************************/

    /// <summary>
    /// فعال و غیر فعال بودن آیتمی از ساب منوی پروژه را برمی گرداند
    /// </summary>
    /// 
    public bool GetEnabled(string MenuTitle)
    {
        Title = MenuTitle;
        TitleNo = GetTitleNos(Title);
        return CheckFormula();
    }

    /// <summary>
    /// آدرس صفحه مربوط به آیتمی از ساب منوی پروژه را برمی گرداند
    /// </summary>
    /// 
    public string GetRedirectLink(string MenuTitle, string PrjReId, string PageMode, string GrdFlt, string SrchFlt)
    {
        string Qs = "ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) + "&PrjReId=" + PrjReId + "&PageMode=" + PageMode
                    + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;

        switch (MenuTitle)
        {
            case "BaseInfo":
                return ("ProjectInsert.aspx?" + Qs);
                break;

            case "RegisteredNo":
                return ("RegisteredNo.aspx?" + Qs);
                break;

            case "PlansMethod":
                return ("PlansMethod.aspx?" + Qs);
                break;

            case "Block":
                return ("Block.aspx?" + Qs);
                break;

            case "Insurance":
                return ("Insurance.aspx?" + Qs);
                break;

            default:
                return "";
        }
    }

    public string GetRedirectLink(string MenuTitle, string PrjReId, string PageMode)
    {
        string Qs = "ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) + "&PrjReId=" + PrjReId + "&PageMode=" + PageMode;

        switch (MenuTitle)
        {
            case "BaseInfo":
                return ("ProjectInsert.aspx?" + Qs);
                break;

            case "RegisteredNo":
                return ("RegisteredNo.aspx?" + Qs);
                break;

            case "PlansMethod":
                return ("PlansMethod.aspx?" + Qs);
                break;

            case "Block":
                return ("Block.aspx?" + Qs);
                break;

            case "Insurance":
                return ("Insurance.aspx?" + Qs);
                break;

            default:
                return "";
        }
    }

    //**********************************************************************************************
    public static ProjectMenusViewPermission CheckProjectMenusViewPermission()
    {
        ProjectMenusViewPermission PrjMenuPer = new ProjectMenusViewPermission();
        TSP.DataManager.Permission perProject = TSP.DataManager.TechnicalServices.ProjectManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perRegisteredNo = TSP.DataManager.TechnicalServices.RegisteredNoManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perPlansMethod = TSP.DataManager.TechnicalServices.PlansMethodManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perBlock = TSP.DataManager.TechnicalServices.BlockManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perInsurance = TSP.DataManager.TechnicalServices.InsuranceManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        PrjMenuPer.CanViewBaseInfo=perProject.CanView;
        PrjMenuPer.CanViewRegisteredNo = perRegisteredNo.CanView;
        PrjMenuPer.CanViewPlansMethod=perPlansMethod.CanView;
        PrjMenuPer.CanViewBlock = perBlock.CanView;
        PrjMenuPer.CanViewInsurance = perInsurance.CanView;

        return PrjMenuPer;
    }
}
