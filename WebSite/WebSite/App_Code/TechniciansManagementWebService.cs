using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

/// <summary>
/// Summary description for TechniciansManagementWebService
/// </summary>

[WebService(Namespace = "http://www.fceo.ir/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class TechniciansManagementWebService : System.Web.Services.WebService
{

    private string _UserName
    {
        get
        {
            return "SNMFTechnicians";
        }
    }

    private string _PassWord
    {
        get
        {
            return "SNMFTechnicians";
        }
    }


    public TechniciansManagementWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = "به ترتیب 0 پیغام خطا 1 نام 2 شماره عضویت 3 شماره پروانه اشتغال 4 پایه نظارت 5 پایه طراحی 6 تاریخ اعتبار پروانه 7 نام رشته 8 کد دفتر طراحی و 9 تاریخ اعتبار دفتر طراحی")]
    public string[] GetDocMemberInfo(Int32 MeId, string UserName, string PassWord)
    {
        string[] Info;
        if (UserName != _UserName || PassWord != _PassWord)
        {
            Info = new string[1];
            Info[0] = "Wrong UserName Or Password";
            return Info;
        }

        Info = new string[10];

        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        DataTable dtMemberInfo = MemberManager.DocMemberInfoWebService(MeId);
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        DataTable dtEngOffice = OfMeManager.FindEngOfficeMemberByPersonId(MeId, (int)TSP.DataManager.EngOfficeConfirmationType.Confirmed, 1,0);
        if (dtMemberInfo.Rows.Count > 1)
        {
            Info = new string[1];
            Info[0] = "رخداد خطا-به ازای این کد عضویت بیش از یک ردیف اطلاعاتی در قسمت عضویت بازگردانده شده است";
            return Info;
        }
        if (dtMemberInfo.Rows.Count == 0)
        {
            Info = new string[1];
            Info[0] = "رخداد خطا- به ازای این کد عضویت در قسمت عضویت ردیف اطلاعاتی یافت نشد";
            return Info;
        }
        //اطلاعات جدول عضویت
        if (!Utility.IsDBNullOrNullValue(dtMemberInfo.Rows[0]["Name"]))
            Info[1] = dtMemberInfo.Rows[0]["Name"].ToString();
        if (!Utility.IsDBNullOrNullValue(dtMemberInfo.Rows[0]["MeNo"]))
            Info[2] = dtMemberInfo.Rows[0]["MeNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(dtMemberInfo.Rows[0]["FileNo"]))
            Info[3] = dtMemberInfo.Rows[0]["FileNo"].ToString();
        if (!Utility.IsDBNullOrNullValue(dtMemberInfo.Rows[0]["Observesion"]))
            Info[4] = dtMemberInfo.Rows[0]["Observesion"].ToString();
        if (!Utility.IsDBNullOrNullValue(dtMemberInfo.Rows[0]["Designer"]))
            Info[5] = dtMemberInfo.Rows[0]["Designer"].ToString();
        if (!Utility.IsDBNullOrNullValue(dtMemberInfo.Rows[0]["FileDate"]))
            Info[6] = dtMemberInfo.Rows[0]["FileDate"].ToString();
        if (!Utility.IsDBNullOrNullValue(dtMemberInfo.Rows[0]["MjName"]))
            Info[7] = dtMemberInfo.Rows[0]["MjName"].ToString();
        
        if (dtEngOffice.Rows.Count > 1 )
        {
           // Info = new string[1];
            Info[0] = "رخداد خطا-اطلاعات عضویت درست است اما به ازای این کد عضویت بیش از یک ردیف اطلاعاتی در قسمت دفاتر بازگردانده شده است";
            return Info;
        }
        if (dtEngOffice.Rows.Count == 0)
        {
          //  Info = new string[1];
            Info[0] = "رخداد خطا-اطلاعات عضویت درست است اما به ازای این کد عضویت ردیف اطلاعاتی در قسمت دفاتر یافت نشد";
            return Info;
        }
       
       
        //اطلاعات جدول دفاتر
        if (dtEngOffice.Rows.Count == 1)
        {
            if (!Utility.IsDBNullOrNullValue(dtEngOffice.Rows[0]["ofId"]))
                Info[8] = dtEngOffice.Rows[0]["ofId"].ToString();
            if (!Utility.IsDBNullOrNullValue(dtEngOffice.Rows[0]["LastEngoffExpDate"]))
                Info[9] = dtEngOffice.Rows[0]["LastEngoffExpDate"].ToString();
        }

        Info[0] = "اطلاعات با موفقیت بازیابی شد.";
        return Info;
    }

     [WebMethod]
    public ProjectForWebService ObsPermitInfo(string UserNameC, string PassWordC, int ProjectId, bool HasCommited)
    {
        ProjectForWebService project = new ProjectForWebService();
        try
        {
        
        if (UserNameC != _UserName || PassWordC != _PassWord)
        {
            project.Message = "Incorrect UserName Or Password";
            project.Status = "-2";
            return project;
        }
       
        if ( ProjectId<1)
        {
            project.Message = "کد پروزه معتبر نمی باشد.";
           project.Status = "-1";
           return project;
        }
      
        TSP.DataManager.TechnicalServices.AccountingManager AccountingManager = new TSP.DataManager.TechnicalServices.AccountingManager();
        AccountingManager.SelectAccountingForProject(ProjectId, -1, (int)TSP.DataManager.TSProjectIngridientType.Observer);
       
        if (AccountingManager.Count <= 0)
        {
            project.Message = "امکان صدور نامه شهرداری وجود ندارد. فیش دستمزد ناظرین وارد نشده است.";
            project.Status = "0";
            return project;
        }

        for (int i = 0; i < AccountingManager.Count; i++)
        {
            if (Convert.ToInt32(AccountingManager[i]["Status"]) != (int)TSP.DataManager.TSAccountingStatus.Payment)
            {
                project.Message = "امکان صدور نامه شهرداری وجود ندارد. فیش دستمزد ناظرین پرداخت نشده است.";
                project.Status = "0";
                return project;
            }
        }
 
        if (HasCommited)
            project.CommitedText = "ضمنا به پیوست یک نسخه تعهد نامه مالک یا وکیل قانونی پلاک فوق الذکر در خصوص معرفی سازنده ذیصلاح جهت اقدامات مقتضی ایفاد می گردد";
            TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
            TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();
           
            int PrjReId = -2;
            ProjectRequestManager.SelectRequestLastVersion(ProjectId, -1, -1);
            if (ProjectRequestManager.Count <= 0)
            {
                project.Message = "برای پروژه مورد نظر درخواستی در سیستم ثبت نشده است.";
                project.Status = "-3";
                return project;
            }
            PrjReId = Convert.ToInt32(ProjectRequestManager[0]["PrjReId"]);
            project.PrjId = ProjectId.ToString();
            DataTable dtProject = ProjectRequestManager.GetProjectInfo(PrjReId);
            if (dtProject.Rows.Count != 1)
            {
                project.Message = "برای پروژه مورد نظر اطلاعات بدرستی بازیابی نمی شود.";
                project.Status = "-3";
                return project;
            }
            
            project.ComputerCode = dtProject.Rows[0]["ComputerCode"].ToString();
            project.RegisteredNo = dtProject.Rows[0]["RegisteredNo"].ToString();
            project.MunName = dtProject.Rows[0]["MunName"].ToString();
            project.OwnerName = dtProject.Rows[0]["OwnerName"].ToString();
            project.OwnerMobileNo = dtProject.Rows[0]["OwnerMobileNo"].ToString();
            int Foundation = Convert.ToInt32(dtProject.Rows[0]["Foundation"]);
            project.Fondation= dtProject.Rows[0]["Foundation"].ToString();
            project.Step = dtProject.Rows[0]["MaxStageNum"].ToString(); 

            project.MainSection = dtProject.Rows[0]["MainSection"].ToString();
            project.MainRegion = dtProject.Rows[0]["MainRegion"].ToString();   
            project.FishNumber = AccountingManager[0]["Number"].ToString();     //اطلاعات اولین فیش را چاپ می کنیم هیچ دلیل مشخصی نیز ندارد با خانم طیبی صحبت شد 
            project.FishDate = AccountingManager[0]["Date"].ToString();
            int FishType = Convert.ToInt32(AccountingManager[0]["Type"]);
            if (FishType == (int)TSP.DataManager.TSAccountingPaymentType.Fiche) project.FishType = "فیش نقدی";
            else if (FishType == (int)TSP.DataManager.TSAccountingPaymentType.Cheque) project.FishType = "چک";
            else if (FishType == (int)TSP.DataManager.TSAccountingPaymentType.POS) project.FishType = "دستگاه کارتخوان";
            else if (FishType == (int)TSP.DataManager.TSAccountingPaymentType.EPayment) project.FishType = "پرداخت الکترونیکی";
            int FundationDifference = 0;
            DataTable dtPreRrjRest = ProjectRequestManager.SelectPreviousProjectRequestStageAndFoundation(ProjectId, PrjReId);
            if (dtPreRrjRest.Rows.Count == 1)
            {
                int PreFoundation = Convert.ToInt32(dtPreRrjRest.Rows[0]["Foundation"]);
                FundationDifference = Foundation - PreFoundation;             
            }
            project.FundationDiff = FundationDifference.ToString();
            TSP.DataManager.TechnicalServices.Project_ObserversManager Project_ObserversManager = new TSP.DataManager.TechnicalServices.Project_ObserversManager();
            DataTable dtObs = Project_ObserversManager.SelectProjectObserverReport(ProjectId,-1, -1).ToTable();
            int count = dtObs.Rows.Count;
            ObsPermit[] ObsPermitArray = new ObsPermit[count];
            for (int i = 0; i < count; i++)
            {
                ObsPermit obsPermit = new ObsPermit();
                obsPermit.RowNumber = dtObs.Rows[i]["RowNumber"].ToString();
                obsPermit.Name = dtObs.Rows[i]["Name"].ToString();
                obsPermit.No = dtObs.Rows[i]["No"].ToString();
                obsPermit.MjName = dtObs.Rows[i]["MjName"].ToString();
                obsPermit.ObsGrdName = dtObs.Rows[i]["ObsGrdName"].ToString();
                obsPermit.ArchitectorCode = dtObs.Rows[i]["ArchitectorCode"].ToString();
                obsPermit.MemberCapacity = dtObs.Rows[i]["MemberCapacity"].ToString();
                obsPermit.IsCoordinator = dtObs.Rows[i]["IsMother"].ToString();
                obsPermit.ObserverType = dtObs.Rows[i]["ObserverType"].ToString();
                
                ObsPermitArray[i] = obsPermit;
            }
            project.ObsPermitS = ObsPermitArray;
            project.Message = "واکشی موفق";
            project.Status = "1";
            return project;
         
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
        }
        project.Message = "خطایی رخ داده است";
        project.Status = "-4";
        return project;
        
    }

     [WebMethod]
     public ProjectForWebService DesignPermitInfo(string UserNameC, string PassWordC, int ProjectId)
     {
         ProjectForWebService project = new ProjectForWebService();
         try
         {

             if (UserNameC != _UserName || PassWordC != _PassWord)
             {
                 project.Message = "Incorrect UserName Or Password";
                 project.Status = "-2";
                 return project;
             }

             if (ProjectId < 1)
             {
                 project.Message = "کد پروزه معتبر نمی باشد.";
                 project.Status = "-1";
                 return project;
             }
             TSP.DataManager.TechnicalServices.BlockManager BlockManager = new TSP.DataManager.TechnicalServices.BlockManager();
             TSP.DataManager.TechnicalServices.ProjectRequestManager ProjectRequestManager = new TSP.DataManager.TechnicalServices.ProjectRequestManager();

             int PrjReId = -2;
             ProjectRequestManager.SelectRequestLastVersion(ProjectId, -1, -1);
             if (ProjectRequestManager.Count <= 0)
             {
                 project.Message = "برای پروژه مورد نظر درخواستی در سیستم ثبت نشده است.";
                 project.Status = "-3";
                 return project;
             }
             PrjReId = Convert.ToInt32(ProjectRequestManager[0]["PrjReId"]);
             project.PrjId = ProjectId.ToString();
             DataTable dtProject = ProjectRequestManager.GetProjectInfo(PrjReId);
             if (dtProject.Rows.Count != 1)
             {
                 project.Message = "برای پروژه مورد نظر اطلاعات بدرستی بازیابی نمی شود.";
                 project.Status = "-3";
                 return project;
             }
            project.ComputerCode = dtProject.Rows[0]["ComputerCode"].ToString();
            project.RegisteredNo = dtProject.Rows[0]["RegisteredNo"].ToString();
             project.MunName = dtProject.Rows[0]["MunName"].ToString();
             project.OwnerName = dtProject.Rows[0]["OwnerName"].ToString();
            project.OwnerMobileNo = dtProject.Rows[0]["OwnerMobileNo"].ToString();
            project.Fondation = dtProject.Rows[0]["Foundation"].ToString();
            int Foundation = Convert.ToInt32(dtProject.Rows[0]["Foundation"]);
             project.MainSection = dtProject.Rows[0]["MainSection"].ToString();
             project.MainRegion = dtProject.Rows[0]["MainRegion"].ToString();    
             project.Step = dtProject.Rows[0]["MaxStageNum"].ToString();
            int FundationDifference = 0;
            DataTable dtPreRrjRest = ProjectRequestManager.SelectPreviousProjectRequestStageAndFoundation(ProjectId, PrjReId);
            if (dtPreRrjRest.Rows.Count == 1)
            {
                int PreFoundation = Convert.ToInt32(dtPreRrjRest.Rows[0]["Foundation"]);
                FundationDifference = Foundation - PreFoundation;
            }
            project.FundationDiff = FundationDifference.ToString();
            TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager ProjectOfficeMembersManager = new TSP.DataManager.TechnicalServices.ProjectOfficeMembersManager();
             DataView dvProjOffMember = ProjectOfficeMembersManager.SelectProjectMembersWithCapacity(ProjectId, "(0)", 0, -1);

             DesPermit[] DesPermitArray = new DesPermit[dvProjOffMember.Count];
             for (int i = 0; i < dvProjOffMember.Count; i++)
             {
                 DesPermit desPermit = new DesPermit();
                 desPermit.RowNumber = dvProjOffMember[i]["RowNumber"].ToString();
                 desPermit.Name = dvProjOffMember[i]["MemberName"].ToString();
                 desPermit.FileMjName = dvProjOffMember[i]["MjName"].ToString();
                 desPermit.DesGrdName = dvProjOffMember[i]["DesGrdName"].ToString();
                 desPermit.FileNo = dvProjOffMember[i]["MemberFileNo"].ToString();
                 desPermit.MemberCapacity = dvProjOffMember[i]["Wage"].ToString();
                 desPermit.FishNo = dvProjOffMember[i]["Number"].ToString();                   
                 DesPermitArray[i] = desPermit;
             }
             project.DesPermit = DesPermitArray;
             project.Message = "واکشی موفق";
             project.Status = "1";
             return project;

         }
         catch (Exception err)
         {
             Utility.SaveWebsiteError(err);
         }
         project.Message = "خطایی رخ داده است";
         project.Status = "-4";
         return project;

     }
}

public class ProjectForWebService
{
    public string Status, Message,PrjId, RegisteredNo, MunName, OwnerName, Fondation,Step,FishNumber,FishDate,FishType, CommitedText, MainSection, MainRegion, OwnerMobileNo, FundationDiff, ComputerCode;
    public ObsPermit[] ObsPermitS;
    public DesPermit[] DesPermit;
}

public class ObsPermit
{
    public string RowNumber, Name, No, MjName, ObsGrdName, ArchitectorCode, MemberCapacity,IsCoordinator,ObserverType;
}

public class DesPermit
{
    public string RowNumber, Name, FileMjName, DesGrdName, FileNo, MemberCapacity, FishNo;
}
