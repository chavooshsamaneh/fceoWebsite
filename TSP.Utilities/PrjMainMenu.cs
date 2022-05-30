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
/// توابع مربوط به منوی پروژه
/// </summary>
/// 
public class PrjMainMenu
{
    public class ProjectMainMenusViewPermission
    {
        public bool CanViewProject;
        public bool CanViewOwner;
        public bool CanViewPlans;
        public bool CanViewObservers;
        public bool CanViewImplementer;
        public bool CanViewContract;
        public bool CanViewTiming;
        public bool CanViewBuildingsLicense;
        public bool CanViewStatusAnnouncement;
        public bool CanViewTSAccounting;
        public bool CanViewDesigner;
    }

    private string Title, CurrentTitle;
    int TitleNo, CurrentTitleNo, ProjectId;

    private enum MenuTitle
    {
        Project ,
        Owner ,
        Accounting,
        Plans,
        Designer,
        Observers ,
        Implementer,
        Contract,
        Timing,
        BuildingsLicense,
        StatusAnnouncement 
    }

    public PrjMainMenu(string CurrentMenuTitle, int PrjId)
    {
        CurrentTitle = CurrentMenuTitle;
        CurrentTitleNo = GetTitleNos(CurrentTitle);
        ProjectId = PrjId;
    }

    private int GetTitleNos(string Name)
    {
        int No = 0;
        switch (Name)
        {
            case "Project":
                No = 1;
                break;

            case "Owner":
                No = 2;
                break;

            case "Accounting":
                No = 3;
                break;

            case "Plans":
                No = 4;
                break;

            case "Designer":
                No = 5;
                break;

            case "Observers":
                No = 6;
                break;

            case "Implementer":
                No = 7;
                break;

            case "Contract":
                No = 8;
                break;

            case "Timing":
                No = 9;
                break;

            case "BuildingsLicense":
                No = 10;
                break;

            case "StatusAnnouncement":
                No = 11;
                break;

            default:
                No = 0;
                break;
        }
        return No;
    }
    //???????????????????????????????????????????????????
    private bool CheckFormula()
    {
        //???????????????????????????????????????????????????
        //if (TitleNo <= CurrentTitleNo)
        //    return true;
        //else if (CheckPervExistance() == true)
        //    return true;
        //else
        //    return CheckExistance();
        return true;
    }

    private bool CheckPervExistance()
    {
        Boolean Per = false;
        switch (Title)
        {
            case "Owner":
                Per = CheckProject();
                break;

            case "Plans":
                Per = CheckOwner();
                break;

            case "Observers":
                Per = CheckPlans();
                break;

            case "Implementer":
                Per = CheckObservers();
                break;

            case "Contract":
                Per = CheckImplementer();
                break;

            case "Timing":
                Per = CheckContract();
                break;

            case "BuildingsLicense":
                Per = CheckImplementer();
                break;

            case "StatusAnnouncement":
                Per = CheckBuildingsLicense();
                break;

            case "Accounting":
                if (CheckOwner() || CheckObservers() || CheckImplementer() || CheckPlans())
                    Per = true;
                else
                    Per = false;
                break;

            case "Designer":
                Per = CheckOwner();
                break;

            default:
                Per = false;
                break;
        }
        return Per;
    }

    private bool CheckExistance()
    {
        Boolean Per = false;
        switch (Title)
        {
            case "Project":
                Per = true;
                break;

            case "Owner":
                Per = CheckOwner();
                break;

            case "Plans":
                Per = CheckPlans();
                break;

            case "Observers":
                Per = CheckObservers();
                break;

            case "Implementer":
                Per = CheckImplementer();
                break;

            case "Contract":
                Per = CheckContract();
                break;

            case "Timing":
                Per = CheckTiming();
                break;

            case "BuildingsLicense":
                Per = CheckBuildingsLicense();
                break;

            case "Accounting":
                Per = CheckAccounting();
                break;

            case "Designer":
                Per = CheckDesigner();
                break;
            default:
                Per = false;
                break;
        }
        return Per;
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

    private bool CheckOwner()
    {
        TSP.DataManager.TechnicalServices.OwnerManager OwnerManager = new TSP.DataManager.TechnicalServices.OwnerManager();
        OwnerManager.FindActivesByProjectId(ProjectId);
        if (OwnerManager.Count > 0)
            return true;
        else
            return false;
    }

    private bool CheckPlans()
    {
        TSP.DataManager.TechnicalServices.PlansManager PlansManager = new TSP.DataManager.TechnicalServices.PlansManager();
        PlansManager.FindActivesByProjectId(ProjectId);
        if (PlansManager.Count > 0)
            return true;
        else
            return false;
    }

    private bool CheckObservers()
    {
        TSP.DataManager.TechnicalServices.Project_ObserversManager ObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
        ObserversManager.FindByProjectId(ProjectId);
        if (ObserversManager.Count > 0)
            return true;
        else
            return false;
    }

    private bool CheckImplementer()
    {
        TSP.DataManager.TechnicalServices.ProjectManager ProjectManager = new TSP.DataManager.TechnicalServices.ProjectManager();
        TSP.DataManager.TechnicalServices.Project_ImplementerManager Project_ImplementerManager = new TSP.DataManager.TechnicalServices.Project_ImplementerManager();

        ProjectManager.FindByProjectId(ProjectId);
        if (ProjectManager.Count <= 0) return true;
        int GroupId = Convert.ToInt32(ProjectManager[0]["GroupId"]);
        if (GroupId == (int)TSP.DataManager.TSStructureGroups.C || GroupId == (int)TSP.DataManager.TSStructureGroups.D)
        {
            Project_ImplementerManager.FindActivesByProjectId(ProjectId);
            if (Project_ImplementerManager.Count > 0)
                return true;
            else
                return false;
        }
        else return true;
    }


    private bool CheckContract()
    {
        TSP.DataManager.TechnicalServices.ContractManager ContractManager = new TSP.DataManager.TechnicalServices.ContractManager();
        ContractManager.FindActivesByProjectId(ProjectId);
        if (ContractManager.Count > 0)
            return true;
        else
            return false;
    }
    private bool CheckTiming()
    {
        TSP.DataManager.TechnicalServices.TimingManager TimingManager = new TSP.DataManager.TechnicalServices.TimingManager();
        TimingManager.FindActivesByProjectId(ProjectId);
        if (TimingManager.Count > 0)
            return true;
        else
            return false;
    }
    private bool CheckBuildingsLicense()
    {
        TSP.DataManager.TechnicalServices.BuildingsLicenseManager BuildingsLicenseManager = new TSP.DataManager.TechnicalServices.BuildingsLicenseManager();
        BuildingsLicenseManager.FindByProjectId(ProjectId);
        if (BuildingsLicenseManager.Count > 0)
            return true;
        else
            return false;
    }

    private bool CheckAccounting()
    {
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        AccountingManager.SelectAccountingForProject(ProjectId);
        if (AccountingManager.Count > 0)
            return true;
        else
            return false;
    }

    private bool CheckDesigner()
    {
        TSP.DataManager.TechnicalServices.Project_DesignerManager Project_DesignerManager = new TSP.DataManager.TechnicalServices.Project_DesignerManager();
        Project_DesignerManager.FindByProjectId(ProjectId);
        if (Project_DesignerManager.Count > 0)
            return true;
        else
            return false;
    }
    /****************************************************** Public Methods ******************************************************/

    /// <summary>
    /// فعال و غیر فعال بودن آیتمی از منوی پروژه را برمی گرداند
    /// </summary>
    /// 
    public bool GetEnabled(string MenuTitle)
    {
        Title = MenuTitle;
        TitleNo = GetTitleNos(Title);
        return CheckFormula();
    }

    /// <summary>
    /// آدرس صفحه مربوط به آیتمی از منوی پروژه را برمی گرداند
    /// </summary>
    /// 
    public string GetRedirectLink(string MenuTitle, string PrjReId, string PageMode, string GrdFlt, string SrchFlt)
    {
        string URL = "";
        string Qs = "ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) + "&PrjReId=" + PrjReId + "&PageMode=" + PageMode
              + "&GrdFlt=" + GrdFlt + "&SrchFlt=" + SrchFlt;
        string AccQs = Qs + "&IngT=" + Utility.EncryptQS("-1") + "&tbtId=" + Utility.EncryptQS("-1");

        switch (MenuTitle)
        {
            case "Project":
                URL = "ProjectInsert.aspx?" + Qs;
                break;

            case "Owner":
                URL = "Owner.aspx?" + Qs;
                break;

            case "Plans":
                URL = "Plans.aspx?" + Qs;
                break;

            case "Observers":
                URL = "Observers.aspx?" + Qs;
                break;

            case "Implementer":
                URL = "Implementer.aspx?" + Qs;
                break;

            case "Contract":
                URL = "Contract.aspx?" + Qs;
                break;

            case "Timing":
                URL = "Timing.aspx?" + Qs;
                break;

            case "BuildingsLicense":
                URL = "BuildingsLicense.aspx?" + Qs;
                break;

            case "StatusAnnouncement":
                URL = "StatusAnnouncement.aspx?" + Qs;
                break;

            case "Accounting":
                URL = "ProjectAccounting.aspx?" + AccQs;
                break;

            case "Designer":
                URL = "Designers.aspx?" + Qs;
                break;
            #region ProjectSubMenu
            case "BaseInfo":
                  URL = "ProjectInsert.aspx?" + Qs;
                break;

            case "RegisteredNo":
                   URL = "RegisteredNo.aspx?" + Qs;
                break;

            case "PlansMethod":
                   URL = "PlansMethod.aspx?" + Qs;
                break;

            case "Block":
                   URL = "Block.aspx?" + Qs;
                break;

            case "Insurance":
                  URL = "Insurance.aspx?" + Qs;
                break;
            #endregion
            #region Accounting Submenue
            case "AccDesigner":
                URL = "ProjectAccountingDesigner.aspx?" + Qs;
                break;
            case "AccObserver":
                URL = "ProjectAccountingObserver.aspx?" + Qs;
                break;
            case "AccImp":
                URL = "ProjectAccountingImplementer.aspx?" + Qs;
                break;
                  case "AccOwner":
                URL = "ProjectAccountingOwner.aspx?" + Qs;
                break;
                
            #endregion
            default:
                URL = "";
                break;
        }
        return URL;
    }

    public string GetRedirectLink(string MenuTitle, string PrjReId, string PageMode)
    {
        string URL = "";
        string Qs = "ProjectId=" + Utility.EncryptQS(ProjectId.ToString()) + "&PrjReId=" + PrjReId + "&PageMode=" + PageMode;
        string AccQs = Qs + "&IngT=" + Utility.EncryptQS("-1") + "&tbtId=" + Utility.EncryptQS("-1");

        switch (MenuTitle)
        {
            case "Project":
                URL = "ProjectInsert.aspx?" + Qs;
                break;

            case "Owner":
                URL = "Owner.aspx?" + Qs;
                break;

            case "Plans":
                URL = "Plans.aspx?" + Qs;
                break;

            case "Observers":
                URL = "Observers.aspx?" + Qs;
                break;

            case "Implementer":
                URL = "Implementer.aspx?" + Qs;
                break;

            case "Contract":
                URL = "Contract.aspx?" + Qs;
                break;

            case "Timing":
                URL = "Timing.aspx?" + Qs;
                break;

            case "BuildingsLicense":
                return "BuildingsLicense.aspx?" + Qs;
                break;

            case "StatusAnnouncement":
                URL = "StatusAnnouncement.aspx?" + Qs;
                break;

            case "Accounting":
                URL = "ProjectAccounting.aspx?" + AccQs;
                break;

            case "Designer":
                URL = "Designers.aspx?" + Qs;
                break;

            default:
                URL = "";
                break;
        }
        return URL;
    }
    //**********************************************************************************************
    public static ProjectMainMenusViewPermission CheckProjectMenusViewPermission()
    {
        ProjectMainMenusViewPermission PrjMainMenuPer = new ProjectMainMenusViewPermission();
        TSP.DataManager.Permission perProject = TSP.DataManager.TechnicalServices.ProjectManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perOwner = TSP.DataManager.TechnicalServices.OwnerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perPlan = TSP.DataManager.TechnicalServices.PlansManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perObservers = TSP.DataManager.TechnicalServices.Project_ObserversManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perImplementer = TSP.DataManager.TechnicalServices.Project_ImplementerManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perContract = TSP.DataManager.TechnicalServices.ContractManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perTiming = TSP.DataManager.TechnicalServices.TimingManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perBuildingsLicense = TSP.DataManager.TechnicalServices.BuildingsLicenseManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perProjectRequest = TSP.DataManager.TechnicalServices.ProjectRequestManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perTSAccounting = TSP.DataManager.TechnicalServices.AccountingManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
        TSP.DataManager.Permission perDesigner = TSP.DataManager.TechnicalServices.Designer_PlansManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());

        PrjMainMenuPer.CanViewProject = perProject.CanView;
        PrjMainMenuPer.CanViewOwner = perOwner.CanView;
        PrjMainMenuPer.CanViewPlans = perPlan.CanView;
        PrjMainMenuPer.CanViewObservers = perObservers.CanView;
        PrjMainMenuPer.CanViewImplementer = perImplementer.CanView;
        PrjMainMenuPer.CanViewContract = perContract.CanView;
        PrjMainMenuPer.CanViewTiming = perTiming.CanView;
        PrjMainMenuPer.CanViewBuildingsLicense = perBuildingsLicense.CanView;
        PrjMainMenuPer.CanViewStatusAnnouncement = perProjectRequest.CanView;
        PrjMainMenuPer.CanViewTSAccounting = perTSAccounting.CanView;
        PrjMainMenuPer.CanViewDesigner = perDesigner.CanView;

        return PrjMainMenuPer;
    }
}
