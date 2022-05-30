using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using DevExpress.Web;
using System.IO;
//using DevExpress.
public partial class Employee_Amoozesh_SeminarAttender : System.Web.UI.Page
{

    #region Properties
    int SeId
    {
        set
        {
            HiddenSeId.Value = value.ToString();
        }
        get
        {
            return Convert.ToInt32(HiddenSeId.Value);
        }
    }

    string RMessage
    { get; set; }


    //DataTable dtMeId
    //{
    //    set 
    //    { 

    //        HiddenFieldCall["dt"] = value;
    //    }
    //    get 
    //    {
    //        try { return (DataTable)HiddenFieldCall["dt"]; }
    //        catch
    //        {
    //            return null;
    //        }
    //    }
    //}
    string FileUpload
    {
        get
        {
            try { return HiddenFieldInfo["FileUpload"].ToString(); }
            catch
            {
                return null;
            }

        }
        set
        {
            HiddenFieldInfo["FileUpload"] = value;
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["ExcelDataTable"] = null;
            if (string.IsNullOrEmpty(Request.QueryString["SeId"]))
            {
                Response.Redirect("Seminar.aspx?GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
            }
            string SminarId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["SeId"]).ToString());

            if (string.IsNullOrEmpty(SminarId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }
            else
            {
                SeId = Convert.ToInt32(SminarId);
                ObjdsPeriodRegister.SelectParameters["PPId"].DefaultValue = SeId.ToString();
            }
            TSP.DataManager.Permission per = TSP.DataManager.PeriodRegisterManager.GetUserPermission(Utility.GetCurrentUser_UserId(), (TSP.DataManager.UserType)Utility.GetCurrentUser_LoginType());
            btnView.Enabled = btnView2.Enabled = GridViewPeriodRegister.Visible = per.CanView;
            btnNew.Enabled = btnNew2.Enabled = per.CanNew;
            // HiddenFieldCall["dt"] = new DataTable();
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
        SeminarInfoUserControl.SeId = SeId;
        PopupPresentExcel.ShowOnPageLoad = false;


    }

    protected void btnInActive_Click(object sender, EventArgs e)
    {
        int PRId = -2;
        if (GridViewPeriodRegister.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriodRegister.GetDataRow(GridViewPeriodRegister.FocusedRowIndex);
            PRId = (int)row["PRId"];
        }
        if (PRId == -1)
        {
            ShowMessage("لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        InActivePeriodRegister(PRId);
    }

    protected void btnPresent_Click(object sender, EventArgs e)
    {
        int PRId = -2;
        if (GridViewPeriodRegister.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriodRegister.GetDataRow(GridViewPeriodRegister.FocusedRowIndex);
            PRId = (int)row["PRId"];
        }
        if (PRId == -1)
        {
            ShowMessage("لطفاً برای حذف اطلاعات ابتدا یک رکورد را انتخاب نمائید");
            return;
        }
        ASPxButton btnId = (ASPxButton)sender;
        PresentPeriodRegister(PRId, btnId.ID);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Seminar.aspx?GrdFlt=" + Request.QueryString["GrdFlt"] + "&SrchFlt=" + Request.QueryString["SrchFlt"]);
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (!CheckIfCanNew(SeId))
        {
            return;
        }
        Response.Redirect("AddPeriodRegister.aspx?PgMd=" + Utility.EncryptQS("NewRegister") + "&PRId=" + Utility.EncryptQS("-1") + "&PrPg=" + Utility.EncryptQS("Periods") + "&PPId=" + Utility.EncryptQS(SeId.ToString()));
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int PRId = -1;
        if (GridViewPeriodRegister.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriodRegister.GetDataRow(GridViewPeriodRegister.FocusedRowIndex);
            PRId = (int)row["PRId"];
        }
        if (PRId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        if (!CheckIfCanEdit(PRId))
            return;
        Response.Redirect("AddPeriodRegister.aspx?PgMd=" + Utility.EncryptQS("Edit") + "&PRId=" + Utility.EncryptQS(PRId.ToString()) + "&PrPg=" + Utility.EncryptQS("Periods") + "&PPId=" + Utility.EncryptQS(SeId.ToString()));
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int PRId = -1;
        if (GridViewPeriodRegister.FocusedRowIndex > -1)
        {
            DataRow row = GridViewPeriodRegister.GetDataRow(GridViewPeriodRegister.FocusedRowIndex);
            PRId = (int)row["PRId"];
        }
        if (PRId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        Response.Redirect("AddPeriodRegister.aspx?PgMd=" + Utility.EncryptQS("View") + "&PRId=" + Utility.EncryptQS(PRId.ToString()) + "&PrPg=" + Utility.EncryptQS("Periods") + "&PPId=" + Utility.EncryptQS(SeId.ToString()));
    }

    protected void CallbackPanelPresentExcel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
        UpdateFromExcel();
    }

    protected void GridViewAccountingDetails_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["AccountingId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        GridViewExporter.FileName = "SeminarAttender";

        GridViewExporter.WriteXlsToResponse(true);
    }

    protected void flpExcel_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
    {
        try
        {
            Session["ExcelDataTable"] = null;
            e.CallbackData = SaveImage(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    #endregion

    #region Methods

    private void PresentPeriodRegister(int PRId, string btnId)
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        PeriodRegisterManager.FindByCode(PRId);
        if (PeriodRegisterManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return;
        }
        PeriodRegisterManager[0].BeginEdit();
        if (btnId == "btnPresent" || btnId == "btnPresent2")
            PeriodRegisterManager[0]["IsPresent"] = true;
        if (btnId == "btnAbcent" || btnId == "btnAbcent2")
            PeriodRegisterManager[0]["IsPresent"] = false;
        PeriodRegisterManager[0].EndEdit();
        PeriodRegisterManager.Save();
        GridViewPeriodRegister.DataBind();
        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
    }

    private void InActivePeriodRegister(int PRId)
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        PeriodRegisterManager.FindByCode(PRId);
        if (PeriodRegisterManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.ErrorInSave));
            return;
        }
        PeriodRegisterManager[0].BeginEdit();
        PeriodRegisterManager[0]["InActive"] = 1;
        PeriodRegisterManager[0].EndEdit();
        PeriodRegisterManager.Save();
        GridViewPeriodRegister.DataBind();
        ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.SaveComplete));
    }

    private void ShowMessage(string Message)
    {
        this.DivReport.Visible = true;
        this.LabelWarning.Text = Message;
    }

    private Boolean CheckIfCanEdit(int PRId)
    {
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        PeriodRegisterManager.FindByCode(PRId);
        if (PeriodRegisterManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }
        if (Convert.ToInt32(PeriodRegisterManager[0]["PaymentType"]) == (int)TSP.DataManager.PeriodRegisterPaymentType.EPayment)
        {
            ShowMessage("امکان ویرایش ثبت نام هایی که از طریق پرداخت الکترونیکی انجام شده است وجود ندارد.");
            return false;
        }
        if (Convert.ToInt32(PeriodRegisterManager[0]["InActive"]) == 1)
        {
            ShowMessage("امکان ویرایش ثبت نام لغو شده وجود ندارد.");
            return false;
        }
        if (Convert.ToInt32(PeriodRegisterManager[0]["Status"]) != (int)TSP.DataManager.PeriodPresentStatus.PeriodRegister)
        {
            ShowMessage("امکان ویرایش این دوره وجود ندارد.وضعیت دوره بایستی ''ثبت نام'' باشد");
            return false;
        }
        return true;
    }

    private Boolean CheckIfCanNew(int PPId)
    {
        TSP.DataManager.PeriodPresentManager PeriodPresentManager = new TSP.DataManager.PeriodPresentManager();
        PeriodPresentManager.FindByCode(PPId);
        if (PeriodPresentManager.Count != 1)
        {
            ShowMessage(Utility.Messages.GetMessage(Utility.Messages.MessageTypes.CanNotFindInformations));
            return false;
        }
        if (Convert.ToInt32(PeriodPresentManager[0]["Status"]) != (int)TSP.DataManager.PeriodPresentStatus.PeriodRegister)
        {
            ShowMessage("امکان ثبت نام در این دوره وجود ندارد.وضعیت دوره بایستی ''ثبت نام'' باشد");
            return false;
        }
        return true;
    }

    protected string SaveImage(UploadedFile uploadedFile)
    {
        string ret = "";
        string tempFileName = "";
        string SminarId = Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["SeId"]).ToString());
        if (uploadedFile.IsValid)
        {
            do
            {
                FileInfo FileType = new FileInfo(uploadedFile.PostedFile.FileName);
                ret = "ExcelPresent-SeId-" + SminarId + "-UserId-" + Utility.GetCurrentUser_UserId().ToString() + Path.GetRandomFileName() + FileType.Extension;
            } while (File.Exists(MapPath("~/Image/Amoozesh/ExcelPresent/") + ret) == true);
            tempFileName = "~/Image/Amoozesh/ExcelPresent/" + ret;
            uploadedFile.SaveAs(MapPath(tempFileName), true);
            InvokeExcelSheet(tempFileName);
        }

        return ret;
    }

    protected void InvokeExcelSheet(string tempFileName)
    {

        string absoluteUrl = this.Server.MapPath(tempFileName);

        var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=Excel 12.0;", absoluteUrl);
        OleDbConnection connExcel = new OleDbConnection(connectionString);
        connExcel.Open();
        DataTable dtSheets = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        if (dtSheets.Select().ToList().Exists(sheet => sheet["TABLE_NAME"].ToString() == "Sheet1$"))
        {
            var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
            var ds = new DataSet();

            adapter.Fill(ds, "list");
            DataTable data = ds.Tables["list"];
            //جستجو برای آنکه شامل فیلد مورد نظر باشد در غیر اینصورت کاری صورت گیرد
            if (!data.Columns.Contains("Sessions"))
            {
                RMessage += "فیلدی به نام" + "Sessions" + "در فرم اکسل وجود ندارد" + "\n";
                return;
            }
            if (!data.Columns.Contains("meid"))
            {
                RMessage += "فیلدی به نام" + "Meid" + "در فرم اکسل وجود ندارد" + "\n";
                return;
            }
            if (data.Columns.Contains("meid"))
            {
                int Count = data.Rows.Count;
                int value; int incorrectData = 0; int correctData = 0; int DeniedData = 0;
                string DetectededMeids = "";
                string DetectededSessins = "";
                DataTable dtAttenderInfo = new DataTable();
                dtAttenderInfo.Columns.Add("MeId", typeof(int));
                dtAttenderInfo.Columns.Add("TotalTimePresent", typeof(string));
                if (Count != 0)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        //جستجو برای آنکه مقادیر درون لیست معتبر باشد
                        if (data.Rows[i]["meid"] == null || !int.TryParse(data.Rows[i]["meid"].ToString(), out value))
                        {
                            //تعداد مقادیر نامعتبر
                            incorrectData++;
                        }
                        else
                        {
                            //ایجاد لیست دیتا وایجاد رشته ی کد های عضویت برای نمایش به کاربر 
                            correctData++;
                            DetectededMeids += data.Rows[i]["meid"].ToString() + ",";
                            DataRow drAttender = dtAttenderInfo.NewRow();
                            drAttender["MeId"] = data.Rows[i]["meid"].ToString();
                            drAttender["TotalTimePresent"] = data.Rows[i]["Sessions"].ToString();
                            dtAttenderInfo.Rows.Add(drAttender);
                            dtAttenderInfo.AcceptChanges();
                        }
                    }

                    flpExcel.JSProperties["cpExcelMeList"] = DetectededMeids.Remove(DetectededMeids.Length - 1);
                    //flpExcel.JSProperties["cpdtMeList"] = dtAttenderInfo;
                    Session["ExcelDataTable"] = dtAttenderInfo;
                    RMessage += "تعداد کل داده های لیست شده:" + data.Rows.Count + "\n";
                    RMessage += "تعداد مقادیر نامعتبر:" + incorrectData + "\n";
                    RMessage += "تعداد مقادیر معتبر:" + correctData + "\n";
                    RMessage += "لیست کد عضویت های تشخیص داده شده:" + DetectededMeids + "\n";
                    RMessage += "****************************" + "\n";
                    RMessage += "درصورت اطمینان از درستی فایل پردازش شده نتایج پردازش را ذخیره کنید" + "\n";
                    RMessage += "****************************" + "\n";

                }
            }



        }
        else
            RMessage += "مورد نظر یافت نمی شود sheet در فایل ارسالی نام" + "/n";

        flpExcel.JSProperties["cpRMessage"] = RMessage;
    }

    private void UpdateFromExcel()
    {
        if(Session["ExcelDataTable"]==null)
        {
            lblResult.Text += "خطا در بازخوانی اطلاعات ایجاد شده است";
            return;
        }
        DataTable dtAttenderList = (DataTable)Session["ExcelDataTable"];// (flpExcel.JSProperties["cpdtMeList"]);
      //  string[] MeList = (HiddenFieldInfo["ExcelMeList"].ToString()).Split(',');
        //DataTable dt = new DataTable();
        //dt.Columns.Add("MeId", typeof(int));
        //for (int i = 0; i < MeList.Length; i++)
        //{
        //    DataRow dr = dt.NewRow();
        //    dr["MeId"] = MeList[i];
        //    dt.Rows.Add(dr);
        //}
        int DeniedData = 0;
        string DeniedMeids = "";
        int seId = Convert.ToInt32(Utility.DecryptQS(Server.HtmlDecode(Request.QueryString["SeId"]).ToString()));
        //جستجو برای آنکه در دوره حضور داشته باشند و ارجاع به تابع بروزرسانی
        //ارجاع به تابع بروزرسانی
        TSP.DataManager.PeriodRegisterManager PeriodRegisterManager = new TSP.DataManager.PeriodRegisterManager();
        DataTable dtResult = PeriodRegisterManager.UpdatePeriodRegisterForSeminarExcelPresent(seId, dtAttenderList, Utility.GetCurrentUser_UserId());
        DeniedData = dtResult.Rows.Count;
        for (int i = 0; i < DeniedData; i++)
        {
            DeniedMeids += dtResult.Rows[i]["meid"].ToString() + ",";
        }

        if (DeniedData > 0)
        {
            lblResult.Text += "نتایج با وجود تعدادی عدم انطباق ذخیره شد.کدهای عضویتی که باافراد ثبت نام شده انطباق ندارند به شرح زیر است:" + "\n";
            lblResult.Text += "تعداد کد عضویت های که در سمینار ثبت نشده اند:" + DeniedData + "\n";
            lblResult.Text += "لیست کد عضویت های که در سمینار ثبت نشده اند:" + DeniedMeids + "\n";
        }
        else
            lblResult.Text += "تمامی کدهای عضویت پردازش شده ذخیره شدند" + "\n";
        int Start = 0;
        int lenString = 250;
        string column = "Z";
        int row = 1;
        string worksheetName = "Sheet1";

        string absoluteUrl = this.Server.MapPath(HiddenFieldInfo["FileUpload"].ToString());

        var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=\"Excel 8.0;HDR=NO;IMEX=3;READONLY=FALSE\"", absoluteUrl);
        OleDbConnection connExcel = new OleDbConnection(connectionString);
        connExcel.Open();
        int len = lblResult.Text.Length;
        for (int i = 0; i < (len / 250) + 1; i++)
        {

            if (Start + lenString > len)
                lenString = len - Start;

            string commandString = String.Format("UPDATE [{0}${1}{2}:{1}{2}] SET F1='{3}'", worksheetName, column, row.ToString(), lblResult.Text.Substring(Start, lenString));
            OleDbCommand commande = new OleDbCommand(commandString, connExcel);
            commande.ExecuteNonQuery();
            row++;
            Start = ((i + 1) * 250) + 1;
        }

        connExcel.Close();
        connExcel.Dispose();
    }

    #endregion
}