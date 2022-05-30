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
using DevExpress.Web;

public partial class Employee_Amoozesh_PeriodRegister : System.Web.UI.Page
{
    private bool IsPageRefresh = false;
    DataTable dtPeriodRegister;
    #region Properties
    //private string PageMode
    //{
    //    set
    //    {
    //        HiddenFieldEpayment["PageMode"] = value;
    //    }
    //    get
    //    {
    //        return HiddenFieldEpayment["PageMode"].ToString();
    //    }
    //}
    #endregion

    #region Evetns
    protected void Page_Load(object sender, EventArgs e)
    {
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");

        if (!IsPostBack)
        {

            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
        }
        else
        {
            if (!IsCallback && Session["postid"] != null)
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString()) { IsPageRefresh = true; }
                Session["postid"] = System.Guid.NewGuid().ToString(); ViewState["postids"] = Session["postid"];
            }
        }

        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        if (!IsPostBack)
        {
            Session["MeEPayPeriodRegister"] = null;
            if (Session["MeEPayPeriodRegister"] == null)
                CreatePeriodRegisterDataTable();
            //else
            //    BindePeriodRegisterGrid();
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        string InsId = "";
        string PPId = "";
        LinkButton lb = (LinkButton)sender;
        string[] Parameters = lb.CommandArgument.Split(';');
        PPId = Parameters[0].ToString();
        InsId = Parameters[1].ToString();
        Response.Redirect("PeriodsView.aspx?PPId=" + Utility.EncryptQS(PPId) + "&InsId=" + Utility.EncryptQS(InsId) + "&RetPage=" + Utility.EncryptQS("PerReg"));
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (Session["MeEPayPeriodRegister"] == null)
            CreatePeriodRegisterDataTable();
        else
            dtPeriodRegister = (DataTable)Session["MeEPayPeriodRegister"];
        if (Utility.GetCurrentUser_UserId() > 0 && Utility.GetCurrentUser_LoginType() != (int)TSP.DataManager.UserType.Member)
        {
            ShowMessage("تنها اعضای دائم و تایید شده سازمان قادر به ثبت نام در دوره های آموزشی می باشند.");
            return;
        }
        LinkButton lb = (LinkButton)sender;
        int PPId = Convert.ToInt32(lb.CommandArgument);

        if (!CheckIfRepititiveRegister(PPId, TSP.DataManager.PeriodRegisterType.PeriodAndExam))
            return;
        if (!CheckPeriodConditons(PPId, TSP.DataManager.PeriodRegisterType.PeriodAndExam))
            return;
        if (!AddPeriod(PPId, TSP.DataManager.PeriodRegisterType.PeriodAndExam))
            ShowMessage("خطایی در اضافه کردن به لیست ثبت نام ایجاد شده است");

        /*  *****************************************************
      * به دلیل آنکه برای کاربران اضافه شدن به لیست گرید پائین صفحه گیج کننده بوده است 
      * کد دکمه پائین صفحه عینا در اینجا کپی شده و هر دوره جداگانه ثبت نام می کنند
     **********************************************   */
        Response.Redirect("../Accounting/EpaymentMultiplePay.aspx?MPt=" + Utility.EncryptQS("PeriodRegister"));

        /*  **********************************************   */
    }

    protected void btnRegisterTest_Click(object sender, EventArgs e)
    {
        if (Session["MeEPayPeriodRegister"] == null)
            CreatePeriodRegisterDataTable();
        else
            dtPeriodRegister = (DataTable)Session["MeEPayPeriodRegister"];
        if (Utility.GetCurrentUser_UserId() > 0 && Utility.GetCurrentUser_LoginType() != (int)TSP.DataManager.UserType.Member)
        {
            ShowMessage("تنها اعضای دائم و تایید شده سازمان قادر به ثبت نام در دوره های آموزشی می باشند.");
            return;
        }
        LinkButton lb = (LinkButton)sender;
        int PPId = Convert.ToInt32(lb.CommandArgument);

        if (!CheckIfRepititiveRegister(PPId, TSP.DataManager.PeriodRegisterType.PeriodAndExam))
            return;
        if (!CheckPeriodConditons(PPId, TSP.DataManager.PeriodRegisterType.OnlyExam))
            return;
        if (!AddPeriod(PPId, TSP.DataManager.PeriodRegisterType.OnlyExam))
            ShowMessage("خطایی در اضافه کردن به لیست ثبت نام ایجاد شده است");


        /*  *****************************************************
* به دلیل آنکه برای کاربران اضافه شدن به لیست گرید پائین صفحه گیج کننده بوده است 
* کد دکمه پائین صفحه عینا در اینجا کپی شده و هر دوره جداگانه ثبت نام می کنند
**********************************************   */
        Response.Redirect("../Accounting/EpaymentMultiplePay.aspx?MPt=" + Utility.EncryptQS("PeriodRegister"));

        /*  **********************************************   */

    }

    protected void btnPeriodOnly_Click(object sender, EventArgs e)
    {
        if (Session["MeEPayPeriodRegister"] == null)
            CreatePeriodRegisterDataTable();
        else
            dtPeriodRegister = (DataTable)Session["MeEPayPeriodRegister"];
        if (Utility.GetCurrentUser_UserId() > 0 && Utility.GetCurrentUser_LoginType() != (int)TSP.DataManager.UserType.Member)
        {
            ShowMessage("تنها اعضای دائم و تایید شده سازمان قادر به ثبت نام در دوره های آموزشی می باشند.");
            return;
        }
        LinkButton lb = (LinkButton)sender;
        int PPId = Convert.ToInt32(lb.CommandArgument);
        if (!CheckIfRepititiveRegister(PPId, TSP.DataManager.PeriodRegisterType.OnlyPeriod))
            return;
        if (!CheckPeriodConditons(PPId, TSP.DataManager.PeriodRegisterType.PeriodAndExam))// دوره به تنهایی همان شرایط دوره و آزمون را دارد
            return;
        if (!AddPeriod(PPId, TSP.DataManager.PeriodRegisterType.OnlyPeriod))
            ShowMessage("خطایی در اضافه کردن به لیست ثبت نام ایجاد شده است");


        /*  *****************************************************
* به دلیل آنکه برای کاربران اضافه شدن به لیست گرید پائین صفحه گیج کننده بوده است 
* کد دکمه پائین صفحه عینا در اینجا کپی شده و هر دوره جداگانه ثبت نام می کنند
**********************************************   */
        Response.Redirect("../Accounting/EpaymentMultiplePay.aspx?MPt=" + Utility.EncryptQS("PeriodRegister"));

        /*  **********************************************   */

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["MeEPayPeriodRegister"] = null;
        Response.Redirect("../MemberHome.aspx");
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Accounting/EpaymentMultiplePay.aspx?MPt=" + Utility.EncryptQS("PeriodRegister"));
    }

    /*
     * به دلیل آنکه برای کاربران اضافه شدن به لیست گیج کننده بوده است 
     * فعلا غیر فعال شده است و هر دوره جداگانه ثبت نام می کنند
     */
    //protected void GridViewPeriodRegister_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    //{
    //    try
    //    {
    //        e.Cancel = true;
    //        if (Session["MeEPayPeriodRegister"] != null)
    //        {
    //            //GridViewPeriodRegister.DataSource = (DataTable)Session["MeEPayPeriodRegister"];
    //            //GridViewPeriodRegister.DataBind();
    //            dtPeriodRegister = (DataTable)Session["MeEPayPeriodRegister"];
    //        }

    //        int Id = -1;
    //        if (GridViewPeriodRegister.FocusedRowIndex > -1)
    //        {
    //            Id = GridViewPeriodRegister.FocusedRowIndex;
    //        }
    //        if (Id == -1)
    //        {
    //            ShowMessage("لطفاً برای حذف اطلاعات ابتدا یک ردیف را انتخاب نمائید");
    //            return;
    //        }
    //        //int TableTypeId=Convert.ToInt32(e.Values["PPId"]);
    //        dtPeriodRegister.Rows.Find(e.Keys["Id"]).Delete();
    //        Session["MeEPayPeriodRegister"] = dtPeriodRegister;
    //        BindePeriodRegisterGrid();
    //        //EPaymentUC.DeleteAccountingRow(TableTypeId);
    //        //DevExpress.Web.Data.ASPxDataDeletingEventArgs AccountingGridEverntArgs = new DevExpress.Web.Data.ASPxDataDeletingEventArgs();
    //        //AccountingGridEverntArgs.Values["TableTypeId"] = TableTypeId;
    //        //EPaymentUC.GridViewAccounting_RowDeleting(new object(), AccountingGridEverntArgs);
    //    }
    //    catch (Exception err)
    //    {
    //        Utility.SaveWebsiteError(err);
    //        ShowMessage("خطایی در حذف دوره از لیست ثبت نام ایجاد شده است");
    //    }
    //}

    #endregion

    #region Methods
    private void CreatePeriodRegisterDataTable()
    {
        dtPeriodRegister = new DataTable();
        dtPeriodRegister.Columns.Add("Id");
        dtPeriodRegister.Columns["Id"].AutoIncrement = true;
        dtPeriodRegister.Columns["Id"].AutoIncrementSeed = 1;
        dtPeriodRegister.Constraints.Add("PK_ID", dtPeriodRegister.Columns["Id"], true);
        dtPeriodRegister.Columns.Add("PRId");
        dtPeriodRegister.Columns.Add("PPId");
        dtPeriodRegister.Columns.Add("CrsName");
        dtPeriodRegister.Columns.Add("PPCode");
        dtPeriodRegister.Columns.Add("PeriodCost");
        dtPeriodRegister.Columns.Add("TestCost");
        dtPeriodRegister.Columns.Add("MeId");
        dtPeriodRegister.Columns.Add("PaymentType");
        dtPeriodRegister.Columns.Add("PaymentTypeName");
        dtPeriodRegister.Columns.Add("IsMember");
        dtPeriodRegister.Columns.Add("IsPassed");
        dtPeriodRegister.Columns.Add("IsConfirm");
        dtPeriodRegister.Columns.Add("InActive");
        dtPeriodRegister.Columns.Add("IsSeminar");
        dtPeriodRegister.Columns.Add("RegisterType");
        dtPeriodRegister.Columns.Add("RegisterDate");
        dtPeriodRegister.Columns.Add("RegCost");
        Session["MeEPayPeriodRegister"] = dtPeriodRegister;
    }

    protected void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private Boolean AddPeriod(int PPId, TSP.DataManager.PeriodRegisterType PeriodRegisterType)
    {
        try
        {
            int Amount = 0;
            TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
            PeriodPresentManager.FindByCode(PPId);
            if (PeriodPresentManager.Count != 1)
                return false;
            //TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
            DataRow dr = dtPeriodRegister.NewRow();
            dr["PRId"] = -1;
            dr["PPId"] = PPId;
            dr["MeId"] = Utility.GetCurrentUser_MeId();
            dr["PaymentType"] = (int)TSP.DataManager.PeriodRegisterPaymentType.EPayment;
            dr["IsMember"] = 1;
            dr["IsPassed"] = 0;
            dr["IsConfirm"] = 0;
            dr["InActive"] = 0;
            dr["IsSeminar"] = 0;
            dr["RegisterType"] = (int)PeriodRegisterType;
            if (PeriodRegisterType == TSP.DataManager.PeriodRegisterType.OnlyExam)
            {
                dr["PaymentTypeName"] = "آزمون";
                if (!Utility.IsDBNullOrNullValue(PeriodPresentManager[0]["TestCost"]))
                    Amount = Convert.ToInt32(PeriodPresentManager[0]["TestCost"]);
            }
            if (PeriodRegisterType == TSP.DataManager.PeriodRegisterType.PeriodAndExam)
            {
                dr["PaymentTypeName"] = "دوره و آزمون";
                if (!Utility.IsDBNullOrNullValue(PeriodPresentManager[0]["PeriodCost"]))
                    Amount = Convert.ToInt32(PeriodPresentManager[0]["PeriodCost"]);

            }
            if (PeriodRegisterType == TSP.DataManager.PeriodRegisterType.OnlyPeriod)
            {
                TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
                DataTable dtPrReg = PeriodRegisterManager.SelectPeriodRegisterForPeriod(-1, PPId, Utility.GetCurrentUser_MeId());
                dtPrReg.DefaultView.RowFilter = "InActive=0";
                dtPrReg.DefaultView.RowFilter = "IsConfirm=1";
                dtPrReg.DefaultView.RowFilter = "RegisterType=" + ((int)TSP.DataManager.PeriodRegisterType.OnlyExam).ToString();
                if (dtPrReg.DefaultView.Count == 1)
                {
                    dr["PRId"] = dtPrReg.DefaultView[0]["PRId"].ToString();
                    dtPrReg.DefaultView.RowFilter = "";
                }
                dr["RegisterType"] = (int)TSP.DataManager.PeriodRegisterType.PeriodAndExam;
                dr["PaymentTypeName"] = "دوره و آزمون";
                int PeriodCost = 0;
                int TestCost = 0;
                if (!Utility.IsDBNullOrNullValue(PeriodPresentManager[0]["PeriodCost"]))
                    PeriodCost = Convert.ToInt32(PeriodPresentManager[0]["PeriodCost"]);
                if (!Utility.IsDBNullOrNullValue(PeriodPresentManager[0]["TestCost"]))
                    TestCost = Convert.ToInt32(PeriodPresentManager[0]["TestCost"]);
                Amount = PeriodCost - TestCost;

            }
            dr["RegCost"] = Amount;
            dr["RegisterDate"] = Utility.GetDateOfToday();
            dr["CrsName"] = PeriodPresentManager[0]["CrsName"].ToString();
            if (!Utility.IsDBNullOrNullValue(PeriodPresentManager[0]["PeriodCost"]))
                dr["PeriodCost"] = PeriodPresentManager[0]["PeriodCost"];// Convert.ToDouble(PeriodPresentManager[0]["PeriodCost"]).ToString("#,#");
            if (!Utility.IsDBNullOrNullValue(PeriodPresentManager[0]["TestCost"]))
                dr["TestCost"] = PeriodPresentManager[0]["TestCost"];//Convert.ToDouble(PeriodPresentManager[0]["TestCost"]).ToString("#,#");
            dr["PPCode"] = PeriodPresentManager[0]["PPCode"];

            dtPeriodRegister.Rows.Add(dr);
            Session["MeEPayPeriodRegister"] = dtPeriodRegister;
            /*
             * به دلیل آنکه برای کاربران اضافه شدن به لیست گیج کننده بوده است 
             * فعلا غیر فعال شده است و هر دوره جداگانه ثبت نام می کنند
             */
            //BindePeriodRegisterGrid();         
            return true;
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            ShowMessage("خطایی در اضافه کردن دوره به لیست ثبت نام ایجاد شده است");
            return false;
        }
    }

    private Boolean CheckIfRepititiveRegister(int PPId, TSP.DataManager.PeriodRegisterType PeriodRegisterType)
    {
        ///این شرط پایین نیز بر اساس دوره تنها باز غلط می شود باید تصحیح شود
        dtPeriodRegister.DefaultView.RowFilter = "PPId=" + PPId.ToString();
        if (dtPeriodRegister.DefaultView.Count > 0)
        {
            ShowMessage("دوره انتخاب شده را پیش از این به لیست اضافه نموده اید.");
            dtPeriodRegister.DefaultView.RowFilter = "";

            return false;
        }
        dtPeriodRegister.DefaultView.RowFilter = "";
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        DataTable dtPrReg = PeriodRegisterManager.SelectPeriodRegisterForPeriod(-1, PPId, Utility.GetCurrentUser_MeId());
        dtPrReg.DefaultView.RowFilter = "InActive=0";
        if (dtPrReg.DefaultView.Count > 0 && PeriodRegisterType != TSP.DataManager.PeriodRegisterType.OnlyPeriod)
        {
            ShowMessage("پیش از این در دوره انتخاب شده ثبت نام نموده اید.جهت پیگیری اطلاعات پرداخت از طریق مسیر منوی سمت راست>>واحد امور مالی>> مدیریت فیش های پرداخت الکترونیکی اقدام نمایید.");
            dtPrReg.DefaultView.RowFilter = "";
            return false;
        }
        if (PeriodRegisterType == TSP.DataManager.PeriodRegisterType.OnlyPeriod)
        {
            dtPrReg.DefaultView.RowFilter = "";
            dtPrReg.DefaultView.RowFilter = "InActive=0";
            dtPrReg.DefaultView.RowFilter = "IsConfirm=1";
            dtPrReg.DefaultView.RowFilter = "RegisterType=" + ((int)TSP.DataManager.PeriodRegisterType.OnlyExam).ToString();
         
            if (dtPrReg.DefaultView.Count > 1)
            {
                dtPrReg.DefaultView.RowFilter = "";
                ShowMessage("خطایی در بازیابی اطلاعات رخ داده است.");
                return false;
            }
            if (dtPrReg.DefaultView.Count == 0)
            {
                dtPrReg.DefaultView.RowFilter = "";
                ShowMessage("این گزینه تنها مربوط به افرادی است که قبلا آزمون در دوره را انتخاب کرده اند.");
                return false;
            }
        }
        dtPrReg.DefaultView.RowFilter = "";
        return true;
    }

    private Boolean CheckPeriodConditons(int PPId, TSP.DataManager.PeriodRegisterType PeriodRegisterType)
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }


        string PeriodTestDate = PeriodPresentManager[0]["TestDate"].ToString();
        string EndRegisterDate = PeriodPresentManager[0]["EndRegisterDate"].ToString();
        int PType = int.Parse(PeriodPresentManager[0]["PeriodType"].ToString());
        if (PType == (int)TSP.DataManager.PeriodRegisterType.OnlyExam 
            && (PeriodRegisterType == TSP.DataManager.PeriodRegisterType.OnlyPeriod || PeriodRegisterType == TSP.DataManager.PeriodRegisterType.PeriodAndExam))
        {
            ShowMessage("این دوره تنها دارای آزمون می باشد و شما فقط قادر به ثبت نام در آزمون آن می باشید.لطفا گزینه صحیح  را جهت ثبت نام انتخاب نمایید.");
            return false;
        }
        int IsEndDate = string.Compare(EndRegisterDate, Utility.GetDateOfToday());
        int IsTestDate = string.Compare(PeriodTestDate, Utility.GetDateOfToday());
        if (IsEndDate <= 0 && PeriodRegisterType == TSP.DataManager.PeriodRegisterType.PeriodAndExam)
        {
            ShowMessage("به دلیل پایان مهلت ثبت نام دوره قادر به ثبت نام در این دوره نمی باشید.");
            return false;
        }

        if (IsTestDate <= 0 && PeriodRegisterType == TSP.DataManager.PeriodRegisterType.OnlyExam)
        {
            ShowMessage("به دلیل پایان امتحان دوره قادر به ثبت نام در این دوره نمی باشید.");
            return false;
        }

        if (Convert.ToInt32(PeriodPresentManager[0]["RemainCapacity"]) == 0 && PeriodRegisterType == TSP.DataManager.PeriodRegisterType.PeriodAndExam)
        {
            ShowMessage("ظرفیت دوره تکمیل می باشد.شما تنها قادر به ثبت نام در آزمون دوره می باشید.");
            return false;
        }
        return true;
    }

    //private void BindePeriodRegisterGrid()
    //{
    //    if (Session["MeEPayPeriodRegister"] != null)
    //    {
    //        GridViewPeriodRegister.DataSource = (DataTable)Session["MeEPayPeriodRegister"];
    //        GridViewPeriodRegister.DataBind();
    //        dtPeriodRegister = (DataTable)Session["MeEPayPeriodRegister"];
    //   }
    //}
    #endregion
}

