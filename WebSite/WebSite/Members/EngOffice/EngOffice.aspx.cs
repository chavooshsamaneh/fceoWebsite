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

public partial class Members_EngOffice_EngOffice : System.Web.UI.Page
{
    private bool IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
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

        if (!IsPostBack)
        {
            SetHelpAddress();
            Session["EngOfficeId"] = null;
            Session["SendBackDataTable_OffRq"] = "";

            string MeId = Utility.GetCurrentUser_MeId().ToString();
            if (string.IsNullOrEmpty(MeId))
            {
                this.Response.Redirect("~/ErrorPage.aspx?ErrorNo=" + ((int)ErrorCodes.ErrorType.PageInputsNotValid).ToString());
                return;
            }

            OdbEngOffice.SelectParameters[0].DefaultValue = MeId;

            TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
            OfMeManager.FindEngOfficeMemberByPersonId(int.Parse(MeId));
            if (OfMeManager.Count > 0)
            {
                if (Convert.ToInt32(OfMeManager[0]["OfpId"]) == (int)TSP.DataManager.OfficePosition.EngOfficeEmployed)//عضو دفتر
                {
                    btnChange.Enabled = false;
                    btnChange2.Enabled = false;
                    btnDelete.Enabled = false;
                    btnDelete2.Enabled = false;
                    btnEdit.Enabled = false;
                    btnEdit2.Enabled = false;
                    BtnNew.Enabled = false;
                    BtnNew2.Enabled = false;
                    btnReduplicate.Enabled = false;
                    btnReduplicate2.Enabled = false;
                    btnRevival.Enabled = false;
                    btnRevival2.Enabled = false;
                    btnSendNextStep.Enabled = false;
                    btnSendNextStep2.Enabled = false;


                }

            }


            TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
            WorkFlowManager.FindByTableType(-1, (int)TSP.DataManager.WorkFlows.EngOfficeConfirming);
            if (WorkFlowManager.Count == 1)
                ObjdsWorkFlowTask.SelectParameters[0].DefaultValue = WorkFlowManager[0]["WorkFlowId"].ToString();
        }
        this.DivReport.Visible = false;
        this.DivReport.Attributes.Add("onclick", "ChangeVisible(this)");
        this.DivReport.Attributes.Add("onmouseover", "ChangeIcon(this)");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();


        int EOfId = -1;
        int EngOfId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            EngOfId = (int)row["EngOfId"];
        }
        if (EngOfId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }

        try
        {
            int focucedIndex = -1;
            TSP.WebControls.CustomAspxDevGridView grid = (TSP.WebControls.CustomAspxDevGridView)CustomAspxDevGridView1.FindDetailRowTemplateControl(CustomAspxDevGridView1.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (grid == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                return;
            }
            focucedIndex = grid.FocusedRowIndex;
            if (focucedIndex <= -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                return;
            }
            DataRow row = grid.GetDataRow(focucedIndex);
            EOfId = (int)row["EOfId"];

            FileManager.FindByCode(EOfId);
            if (FileManager.Count <= 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات توسط کاربر دیگری تغییر یافته است";
                return;
            }
            if (Convert.ToBoolean(FileManager[0]["Requester"]))
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "درخواست مورد نظر از سمت کارمند ایجاد شده است.امکان ویرایش درخواست برای شما وجود ندارد";
                return;
            }
            if (FileManager[0]["IsConfirm"].ToString() != "0")
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان ویرایش درخواست پاسخ داده شده وجود ندارد";
                return;
            }
            if (CheckPermitionForEdit(EOfId))
                Response.Redirect("EngOfficeRegister.aspx?EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Edit")
                    + "&EngOfId=" + Utility.EncryptQS(EngOfId.ToString()));
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان لغو درخواست در این مرحله از جریان کار برای شما وجود ندارد";
                return;
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
        }
    }
    protected void BtnNew_Click(object sender, EventArgs e)
    {
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        OfMeManager.FindEngOfficeMemberByPersonId(Utility.GetCurrentUser_MeId());
        if (OfMeManager.Count > 0)
        {
            for (int i = 0; i < OfMeManager.Count; i++)
            {
                if (!Convert.ToBoolean(OfMeManager[i]["EngInActive"]) || OfMeManager[i]["EngIsConfirm"].ToString() == "0" || OfMeManager[i]["EngIsConfirm"] == "1")//فعال
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ثبت دفتر جدید برای شما وجود ندارد";
                    return;
                }

            }
        }
        else
            Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS("") + "&PageMode=" + Utility.EncryptQS("New") + "&EOfId=" + Utility.EncryptQS(""));


    }
    protected void btnChange_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (!CheckLocker()) return;
        int EngOfId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            EngOfId = (int)row["EngOfId"];
        }
        if (EngOfId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
            TSP.DataManager.EngOfficeManager EngOffManager = new TSP.DataManager.EngOfficeManager();
            EngOffManager.FindByCode(EngOfId);
            if (EngOffManager.Count > 0)
            {
                if (EngOffManager[0]["IsConfirm"].ToString() == "1")
                {
                    FileManager.FindByEngOffCode(EngOfId, 0, -1);
                    if (FileManager.Count > 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
                        return;
                    }
                    FileManager.FindByEngOffCode(EngOfId, 1, 0);
                    if (FileManager.Count == 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان درخواست تغییرات وجود ندارد.پروانه عضو مورد نظر تایید نشده است";
                        return;
                    }

                    FileManager.FindByEngOffCode(EngOfId, -1, -1);//return last EOfId
                    if (FileManager.Count > 0)
                    {
                        if (FileManager[0]["IsConfirm"].ToString() != "1")
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان درخواست تغییرات وجود ندارد.پروانه عضو مورد نظر تایید نشده است";
                            return;
                        }
                        else
                        {
                            Change(EngOfId, int.Parse(FileManager[0]["EOfId"].ToString()));
                        }
                    }

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ویرایش وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.";

                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }

    }
    protected void btnRevival_Click(object sender, EventArgs e)
    {
        int EngOfId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            EngOfId = (int)row["EngOfId"];
        }
        if (EngOfId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای ویرایش اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
            TSP.DataManager.EngOfficeManager EngOffManager = new TSP.DataManager.EngOfficeManager();
            EngOffManager.FindByCode(EngOfId);
            if (EngOffManager.Count > 0)
            {
                if (EngOffManager[0]["IsConfirm"].ToString() == "1")
                {
                    FileManager.FindByEngOffCode(EngOfId, 0, -1);
                    if (FileManager.Count > 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
                        return;
                    }
                    FileManager.FindByEngOffCode(EngOfId, 1, 0);
                    if (FileManager.Count == 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان درخواست تغییرات وجود ندارد.پروانه عضو مورد نظر تایید نشده است";
                        return;
                    }

                    FileManager.FindByEngOffCode(EngOfId, -1, -1);//return last EOfId
                    if (FileManager.Count > 0)
                    {
                        if (FileManager[0]["IsConfirm"].ToString() != "1")
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان درخواست تغییرات وجود ندارد.پروانه عضو مورد نظر تایید نشده است";
                            return;
                        }
                        else
                        {
                            Revival(EngOfId, int.Parse(FileManager[0]["EOfId"].ToString()));
                        }
                    }

                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ویرایش وجود ندارد.عضویت عضو انتخابی در وضعیت لغو شده یا در جریان می باشد.";

                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (!CheckLocker()) return;

        int EOfId = -1;
        int EngOfId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            EngOfId = (int)row["EngOfId"];
        }
        if (EngOfId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";
            return;
        }
        try
        {
            TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)CustomAspxDevGridView1.FindDetailRowTemplateControl(CustomAspxDevGridView1.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridRequest == null)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                return;
            }
            if (GridRequest.VisibleRowCount <= 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                return;
            }
            int index0 = GridRequest.FocusedRowIndex;
            if (index0 <= -1)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                return;
            }
            EOfId = int.Parse(GridRequest.GetDataRow(index0)["EOfId"].ToString());
            TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
            FileManager.FindByCode(EOfId);
            if (FileManager.Count <= 0)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
                return;
            }
            if (Convert.ToBoolean(FileManager[0]["Requester"]))
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان حذف برای درخواست صادر شده توسط کارمند وجود ندارد";
                return;
            }
            //if (Convert.ToInt32(FileManager[0]["Type"]) == (int)TSP.DataManager.EngOffFileType.SaveFileDocument)//درخواست اولیه
            //{
            //    this.DivReport.Visible = true;
            //    this.LabelWarning.Text = "امکان حذف درخواست اولیه ثبت نام وجود ندارد";
            //    return;
            //}
            if (FileManager[0]["IsConfirm"].ToString() != "0")
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان حذف برای درخواست پاسخ داده شده وجود ندارد";
                return;
            }
            if (CheckPermitionForDelete(EOfId))
                Delete(EngOfId, EOfId);
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "امکان حذف درخواست در این مرحله از جریان کار برای شما وجود ندارد";
            }
        }
        catch (Exception err)
        {
            Utility.SaveWebsiteError(err);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
        }
    }
    protected void btnReduplicate_Click(object sender, EventArgs e)
    {
        if (IsPageRefresh)
            return;
        if (!CheckLocker()) return;
        int EngOfId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            EngOfId = (int)row["EngOfId"];
        }
        if (EngOfId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
            TSP.DataManager.EngOfficeManager EngOffManager = new TSP.DataManager.EngOfficeManager();
            EngOffManager.FindByCode(EngOfId);
            if (EngOffManager.Count > 0)
            {
                if (EngOffManager[0]["IsConfirm"].ToString() == "1")
                {
                    FileManager.FindByEngOffCode(EngOfId, 0, -1);
                    if (FileManager.Count > 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "به دلیل وجود درخواست بررسی نشده امکان ثبت درخواست جدید وجود ندارد";
                        return;
                    }
                    FileManager.FindByEngOffCode(EngOfId, 1, 0);
                    if (FileManager.Count == 0)
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "امکان درخواست المثنی وجود ندارد.برای عضو مورد نظر پروانه صادر نشده است";
                        return;
                    }

                    FileManager.FindByEngOffCode(EngOfId, -1, -1);//return last EOfId
                    if (FileManager.Count > 0)
                    {
                        if (FileManager[0]["IsConfirm"].ToString() != "1")
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان درخواست المثنی وجود ندارد.پروانه عضو مورد نظر تایید نشده است";
                            return;
                        }
                        else
                        {
                            Reduplicate(EngOfId, int.Parse(FileManager[0]["EOfId"].ToString()));
                        }
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "امکان ویرایش وجود ندارد.دفتر انتخابی در وضعیت لغو شده یا در جریان می باشد.";

                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
            }
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {

        int EOfId = -1;
        int EngOfId = -1;

        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow row = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            EngOfId = (int)row["EngOfId"];
        }
        if (EngOfId == -1)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً برای مشاهده اطلاعات ابتدا یک رکورد را انتخاب نمائید";

        }
        else
        {
            try
            {
                TSP.WebControls.CustomAspxDevGridView GridRequest = (TSP.WebControls.CustomAspxDevGridView)CustomAspxDevGridView1.FindDetailRowTemplateControl(CustomAspxDevGridView1.FocusedRowIndex, "CustomAspxDevGridViewRequest");
                if (GridRequest != null)
                {
                    if (GridRequest.VisibleRowCount > 0)
                    {
                        int index0 = GridRequest.FocusedRowIndex;
                        if (index0 != -1)
                        {
                            EOfId = int.Parse(GridRequest.GetDataRow(index0)["EOfId"].ToString());

                            Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()));
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                        }

                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";

                    }


                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";


                }

            }
            catch (Exception err)
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
            }
        }

    }

    private void Change(int EngOfId, int EOfId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.ClearBeforeFill = true;
        FileManager.ClearBeforeFill = true;

        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(FileManager);

        try
        {

            int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
            DataTable dtWfState = WorkFlowStateManager.SelectLastState(TableType, EOfId);
            if (dtWfState.Rows.Count > 0)
            {
                int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
                int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectDocumentOfEngOfficeAndEndProcess;
                int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfEngOfficeAndEndProccess;
                int DocumentUnitConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentEngOffice;

                int RejectTaskId = -1;
                int ConfirmTaskId = -1;
                int DocumentUnitConfirmTaskId = -1;

                WorkFlowTaskManager.FindByTaskCode(DocumentUnitConfirmTaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    DocumentUnitConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                }


                WorkFlowTaskManager.FindByTaskCode(RejectTaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    RejectTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                }

                WorkFlowTaskManager.FindByTaskCode(ConfirmTaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    ConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                }

                if (CurrentTaskId == RejectTaskId || CurrentTaskId == ConfirmTaskId)
                {
                    FileManager.FindByCode(EOfId);
                    if (FileManager.Count == 1)
                    {
                        if (FileManager[0]["IsConfirm"].ToString() != "0")
                        {
                            Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Change") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()));

                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان درخواست تغییرات برای پروانه تایید نشده وجود ندارد.";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به دلیل به پایان نرسیدن جریان کار پروانه انتخاب شده امکان درخواست تغییرات وجود ندارد.";
                }

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "برای پرونده انتخاب شده جریان کاری تعریف نشده است.";
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
        }
    }
    protected void Delete(int EngOfId, int EOfId)
    {
        TSP.DataManager.TransactionManager trans = new TSP.DataManager.TransactionManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.EngOfficeManager EngOfficeManager = new TSP.DataManager.EngOfficeManager();
        TSP.DataManager.OfficeMemberManager MemManager = new TSP.DataManager.OfficeMemberManager();
        TSP.DataManager.AttachmentsManager attachManager = new TSP.DataManager.AttachmentsManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.TelManager telManager = new TSP.DataManager.TelManager();
        TSP.DataManager.AddressManager AddManager = new TSP.DataManager.AddressManager();
        TSP.DataManager.RequestInActivesManager RequestInActivesManager = new TSP.DataManager.RequestInActivesManager();

        trans.Add(FileManager);
        trans.Add(MemManager);
        trans.Add(attachManager);
        trans.Add(WorkFlowStateManager);
        trans.Add(telManager);
        trans.Add(AddManager);
        trans.Add(EngOfficeManager);

        try
        {

            trans.BeginSave();

            MemManager.FindForDelete(EOfId, 1);
            if (MemManager.Count > 0)
            {
                int c = MemManager.Count;
                for (int i = 0; i < c; i++)
                    MemManager[0].Delete();

                MemManager.Save();

            }
            attachManager.FindByTablePrimaryKey(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile), EOfId);
            if (attachManager.Count > 0)
            {
                int c = attachManager.Count;
                for (int i = 0; i < c; i++)
                    attachManager[0].Delete();

                attachManager.Save();
            }
            int WfCode = (int)TSP.DataManager.WorkFlows.EngOfficeConfirming;
            WorkFlowStateManager.SelectByWorkFlowCodeForDelete(WfCode, EOfId);
            if (WorkFlowStateManager.Count > 0)
            {
                WorkFlowStateManager[0].Delete();
                WorkFlowStateManager.Save();
            }
            telManager.FindByTablePrimaryKey(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile), EOfId);
            if (telManager.Count > 0)
            {
                int c = telManager.Count;
                for (int i = 0; i < c; i++)
                    telManager[0].Delete();

                telManager.Save();
            }
            AddManager.FindByTablePrimaryKey(TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOffFile), EOfId);
            if (AddManager.Count > 0)
            {
                AddManager[0].Delete();
                AddManager.Save();
            }

            RequestInActivesManager.FindByTableIdTableType(-1, TSP.DataManager.TableTypeManager.FindTtId(TSP.DataManager.TableType.EngOfficeMember), EOfId);
            if (RequestInActivesManager.Count > 0)
            {
                RequestInActivesManager[0].Delete();
                RequestInActivesManager.Save();
            }

            FileManager.FindByCode(EOfId);
            if (FileManager.Count != 1)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات رخ داده است";
                return;
            }
            int ReqType = Convert.ToInt32(FileManager[0]["Type"]);
            FileManager[0].Delete();
            int cn = FileManager.Save();
            FileManager.DataTable.AcceptChanges();
            if (cn != 1)
            {
                trans.CancelSave();
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                return;
            }

            if (ReqType == (int)TSP.DataManager.EngOffFileType.SaveFileDocument)//درخواست اولیه  
            {
                EngOfficeManager.FindByCode(EngOfId);
                EngOfficeManager[0].Delete();
                EngOfficeManager.Save();
                EngOfficeManager.DataTable.AcceptChanges();
            }

            trans.EndSave();
            CustomAspxDevGridView1.DataBind();
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "حذف درخواست با موفقیت انجام شد";

        }
        catch (Exception err)
        {
            trans.CancelSave();
            Utility.SaveWebsiteError(err);
            if (err.GetType() == typeof(System.Data.SqlClient.SqlException))
            {
                System.Data.SqlClient.SqlException se = (System.Data.SqlClient.SqlException)err;
                if (se.Number == 547)
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به علت وجود اطلاعات وابسته امکان حذف نمی باشد.";
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "خطایی در حذف انجام گرفته است";
            }
            if (Utility.ShowExceptionError())
                this.LabelWarning.Text += err.Message;
        }


    }
    private void Reduplicate(int EngOfId, int EOfId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.ClearBeforeFill = true;
        FileManager.ClearBeforeFill = true;

        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(FileManager);

        try
        {

            int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
            DataTable dtWfState = WorkFlowStateManager.SelectLastState(TableType, EOfId);
            if (dtWfState.Rows.Count > 0)
            {
                int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
                int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectDocumentOfEngOfficeAndEndProcess;
                int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfEngOfficeAndEndProccess;
                int DocumentUnitConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentEngOffice;

                int RejectTaskId = -1;
                int ConfirmTaskId = -1;
                int DocumentUnitConfirmTaskId = -1;

                WorkFlowTaskManager.FindByTaskCode(DocumentUnitConfirmTaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    DocumentUnitConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                }


                WorkFlowTaskManager.FindByTaskCode(RejectTaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    RejectTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                }

                WorkFlowTaskManager.FindByTaskCode(ConfirmTaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    ConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                }

                if (CurrentTaskId == RejectTaskId || CurrentTaskId == ConfirmTaskId)
                {
                    FileManager.FindByCode(EOfId);
                    if (FileManager.Count == 1)
                    {
                        if (FileManager[0]["IsConfirm"].ToString() != "0")
                        {
                            Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("Reduplicate") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()));

                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان درخواست المثنی برای پروانه تایید نشده وجود ندارد.";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به دلیل به پایان نرسیدن جریان کار پروانه انتخاب شده امکان درخواست المثنی وجود ندارد.";
                }

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "برای پرونده انتخاب شده جریان کاری تعریف نشده است.";
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در ذخیره انجام گرفته است.";
        }
    }
    private void Revival(int EngOfId, int EOfId)
    {
        TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        WorkFlowTaskManager.ClearBeforeFill = true;
        FileManager.ClearBeforeFill = true;

        TransactionManager.Add(WorkFlowStateManager);
        TransactionManager.Add(FileManager);

        try
        {


            int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
            DataTable dtWfState = WorkFlowStateManager.SelectLastState(TableType, EOfId);
            if (dtWfState.Rows.Count > 0)
            {
                int CurrentTaskId = int.Parse(dtWfState.Rows[0]["TaskId"].ToString());
                int RejectTaskCode = (int)TSP.DataManager.WorkFlowTask.RejectDocumentOfEngOfficeAndEndProcess;
                int ConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.ConfirmDocumentOfEngOfficeAndEndProccess;
                int DocumentUnitConfirmTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentUnitEmployeeConfirmingDocumentEngOffice;
                int RejectTaskId = -1;
                int ConfirmTaskId = -1;
                int DocumentUnitConfirmTaskId = -1;

                WorkFlowTaskManager.FindByTaskCode(DocumentUnitConfirmTaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    DocumentUnitConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                }


                WorkFlowTaskManager.FindByTaskCode(RejectTaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    RejectTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                }

                WorkFlowTaskManager.FindByTaskCode(ConfirmTaskCode);
                if (WorkFlowTaskManager.Count > 0)
                {
                    ConfirmTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
                }

                if (CurrentTaskId == RejectTaskId || CurrentTaskId == ConfirmTaskId)
                {
                    FileManager.FindByCode(EOfId);
                    if (FileManager.Count == 1)
                    {
                        if (FileManager[0]["IsConfirm"].ToString() != "0")
                        {
                            string CrtEndDate = FileManager[0]["ExpireDate"].ToString();
                            Utility.Date objDate = new Utility.Date(CrtEndDate);
                            string LastMonth = objDate.AddMonths(-1);
                            string Today = Utility.GetDateOfToday();
                            int IsDocExp = string.Compare(Today, LastMonth);
                            if (IsDocExp > 0)
                            {
                                Response.Redirect("EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" 
                                    + Utility.EncryptQS("Revival") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()));

                            }
                            else
                            {
                                this.DivReport.Visible = true;
                                this.LabelWarning.Text = "تاریخ اعتبار پروانه انتخاب شده به پایان نرسیده است.امکان تمدید وجود ندارد";

                            }
                        }
                        else
                        {
                            this.DivReport.Visible = true;
                            this.LabelWarning.Text = "امکان تمدید برای پروانه تایید نشده وجود ندارد.";
                        }
                    }
                    else
                    {
                        this.DivReport.Visible = true;
                        this.LabelWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                    }
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "به دلیل به پایان نرسیدن جریان کار پروانه انتخاب شده امکان درخواست تمدید وجود ندارد.";
                }

            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "برای پرونده انتخاب شده جریان کاری تعریف نشده است.";
            }
        }
        catch (Exception err)
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "خطایی در بازخوانی اطلاعات انجام گرفته است.";
        }
    }
    bool CheckLocker()
    {
        int Meid = Utility.GetCurrentUser_MeId();
        TSP.DataManager.MemberManager MemberManager = new TSP.DataManager.MemberManager();
        MemberManager.FindByCode(Meid);
        if (Convert.ToBoolean(MemberManager[0]["IsLock"]))
        {
            TSP.DataManager.LockHistoryManager lockHistoryManager = new TSP.DataManager.LockHistoryManager();
            string MemberLockers = lockHistoryManager.FindLockers(Meid, 0, 1);

            string lockers = Utility.GetFormattedObject(MemberLockers);
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "به دلیل قفل بودن وضعیت عضویت شما توسط  " + lockers + " امکان ثبت اطلاعات وجود ندارد.";
            return false;
        }
        return true;
    }
    void SetHelpAddress()
    {
        HiddenHelp["HelpAddress"] = "../Help/ShowHelp.aspx?Id=" + Utility.EncryptQS(((int)Utility.Help.HelpFiles.PortalEngOfficeDocument).ToString());
    }

    private Boolean CheckPermitionForEdit(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        int TaskOrder = -1;
        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
        WorkFlowTaskManager.FindByTaskCode(TaskCode);
        if (WorkFlowTaskManager.Count > 0)
        {
            TaskOrder = int.Parse(WorkFlowTaskManager[0]["TaskOrder"].ToString());
            if (TaskOrder != 0)
            {
                int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
                DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
                    int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
                    int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
                    int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
                    int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
                    int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;

                    if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
                    {
                        DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                        if (dtWorkFlowState.Rows.Count > 0)
                        {
                            int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                            int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                            int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());
                            if (FirstTaskCode == DocMeFileSaveInfoTaskCode)
                            {
                                if (FirstNmcIdType == 1)
                                {
                                    if (FirstNmcId == Utility.GetCurrentUser_MeId())
                                    {
                                        return true;
                                    }

                                }

                            }

                        }

                    }

                }

            }

        }
        return false;

    }
    private Boolean CheckPermitionForDelete(int TableId)
    {
        TSP.DataManager.TaskDoerManager TaskDoerManager = new TSP.DataManager.TaskDoerManager();
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();

        int TaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
        int WfCode = (int)TSP.DataManager.WorkFlows.EngOfficeConfirming;
        DataTable dtState = WorkFlowStateManager.SelectByWorkFlowCode(WfCode, TableId);
        if (dtState.Rows.Count == 1)
        {
            int CurrentTaskCode = int.Parse(dtState.Rows[0]["TaskCode"].ToString());
            int CurrentNmcId = int.Parse(dtState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtState.Rows[0]["NmcIdType"].ToString());
            int CurrentTaskId = int.Parse(dtState.Rows[0]["TaskId"].ToString());
            if (CurrentNmcId == Utility.GetCurrentUser_MeId() && CurrentNmcIdType == 1)
            {
                if (CurrentTaskCode == TaskCode)
                    return true;
            }
        }
        return false;
    }
    #region WorkFlow
    private void SendMemberFileDocToNextStep(int EOfId, int EngOfId)
    {
        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();

        int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
        int WorkflowCode = (int)TSP.DataManager.WorkFlows.EngOfficeConfirming;
        int SaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;
        int NextStepTaskId = -1;

        DataTable dtNextTopTask = WorkFlowTaskManager.SelectNextTopSteps(TableType, SaveInfoTaskCode, WorkflowCode);
        if (dtNextTopTask.Rows.Count > 0)
        {
            int NextStepTaskCode = int.Parse(dtNextTopTask.Rows[0]["TaskCode"].ToString());
            WorkFlowTaskManager.FindByTaskCode(NextStepTaskCode);
            if (WorkFlowTaskManager.Count == 1)
            {
                NextStepTaskId = int.Parse(WorkFlowTaskManager[0]["TaskId"].ToString());
            }

            DataTable dtSendBack = (DataTable)Session["SendBackDataTable_OffRq"];
            cmbSendBackTask.DataSource = dtSendBack;
            cmbSendBackTask.ValueField = "TaskId";
            cmbSendBackTask.TextField = "TaskName";
            cmbSendBackTask.DataBind();

            int SelectedTaskId = int.Parse(cmbSendBackTask.SelectedItem.Value.ToString());
            if (SelectedTaskId == NextStepTaskId)
            {
                TSP.DataManager.TransactionManager TransactionManager = new TSP.DataManager.TransactionManager();
                TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager(TransactionManager);

                TransactionManager.Add(WorkFlowStateManager);

                int NmcId = Utility.GetCurrentUser_MeId();
                int NmcIdType = 1;
                if (NmcId > 0)
                {
                    TransactionManager.BeginSave();
                    string Url = "<a href='../../Employee/Document/EngOffice/EngOfficeRegister.aspx?EngOfId=" + Utility.EncryptQS(EngOfId.ToString()) + "&PageMode=" + Utility.EncryptQS("View") + "&EOfId=" + Utility.EncryptQS(EOfId.ToString()) + "' target=_blank>اینجا کلیک کنید</a>";

                    string MsgContent = "";
                    int SendDoc = WorkFlowStateManager.SendDocToNextStep(TableType, EOfId, SelectedTaskId, txtDescription.Text, NmcId, NmcIdType, Utility.GetCurrentUser_UserId(), MsgContent, Url);

                    switch (SendDoc)
                    {
                        case -6:
                            TransactionManager.CancelSave();
                            PanelSaveSuccessfully.ClientVisible = true;
                            PanelMain.ClientVisible = false;
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                            lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله جاری وجود ندارد.";
                            break;
                        case -4:
                            TransactionManager.CancelSave();
                            PanelSaveSuccessfully.ClientVisible = true;
                            PanelMain.ClientVisible = false;
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                            lblInstitueWarning.Text = "خطایی در ذخیره انجام شد.";
                            break;
                        case -5:
                            TransactionManager.CancelSave();
                            PanelSaveSuccessfully.ClientVisible = true;
                            PanelMain.ClientVisible = false;
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                            lblInstitueWarning.Text = "برای پرونده انتخاب شده هیچ عملیاتی انجام نشده است.";
                            break;
                        case -8:
                            TransactionManager.CancelSave();
                            PanelSaveSuccessfully.ClientVisible = true;
                            PanelMain.ClientVisible = false;
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                            lblInstitueWarning.Text = "انجام دهنده عملیات بعد نامشخص می باشد.";
                            break;
                        default:
                            TransactionManager.EndSave();
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Green;
                            lblInstitueWarning.Text = "ذخیره انجام شد.";
                            PanelMain.ClientVisible = false;
                            PanelSaveSuccessfully.ClientVisible = true;
                            CustomAspxDevGridView1.DataBind();
                            break;
                    }
                }
                else
                {
                    PanelSaveSuccessfully.ClientVisible = true;
                    PanelMain.ClientVisible = false;
                    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                    lblInstitueWarning.Text = "به علت نامشخص بودن سمت شما در چارت سازمانی قادر به ارسال پرونده به مرحله بعد نمی باشید.";
                }

            }
            else
            {
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "شما قادر به ارسال پرونده به مرحله انتخاب شده نیستید.";

            }
            CustomAspxDevGridView1.DataBind();
        }
        else
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "مرحله بعد جریان کار نا مشخص است.";
        }



    }
    private void SelectSendBackTask(int TableType, int TableId, int EngOfId)
    {
        TSP.DataManager.OfficeMemberManager OfMeManager = new TSP.DataManager.OfficeMemberManager();
        OfMeManager.selectActiveEngOfficeMember(TableId, EngOfId);
        for (int i = 0; i < OfMeManager.Count; i++)
        {
            if ((Utility.IsDBNullOrNullValue(OfMeManager[i]["IsConfirm"])) || (Utility.IsDBNullOrNullValue(OfMeManager[i]["ConfirmDate"])))
            {
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد وجود ندارد.تمامی اعضای دفتر درخواست را پاسخ نداده اند";
                return;
            }
        }

        TSP.DataManager.WorkFlowTaskManager WorkFlowTaskManager = new TSP.DataManager.WorkFlowTaskManager();
        TSP.DataManager.WorkFlowStateManager WorkFlowStateManager = new TSP.DataManager.WorkFlowStateManager();
        TSP.DataManager.WorkFlowManager WorkFlowManager = new TSP.DataManager.WorkFlowManager();
        TSP.DataManager.EngOffFileManager FileManager = new TSP.DataManager.EngOffFileManager();
        WorkFlowStateManager.ClearBeforeFill = true;
        DataTable dtWorkFlowLastState = WorkFlowStateManager.SelectLastState(TableType, TableId);
        if (dtWorkFlowLastState.Rows.Count > 0)
        {
            int CurrentTaskOrder = int.Parse(dtWorkFlowLastState.Rows[0]["TaskOrder"].ToString());
            int CurrentTaskCode = int.Parse(dtWorkFlowLastState.Rows[0]["TaskCode"].ToString());
            int CurrentSubOrder = int.Parse(dtWorkFlowLastState.Rows[0]["SubOrder"].ToString());
            int CurrentNmcId = int.Parse(dtWorkFlowLastState.Rows[0]["NmcId"].ToString());
            int CurrentNmcIdType = int.Parse(dtWorkFlowLastState.Rows[0]["NmcIdType"].ToString());
            int CurrentTaskId = int.Parse(dtWorkFlowLastState.Rows[0]["TaskId"].ToString());
            int CurrentWorkFlowCode = int.Parse(dtWorkFlowLastState.Rows[0]["WorkFlowCode"].ToString());
            int DocMeFileSaveInfoTaskCode = (int)TSP.DataManager.WorkFlowTask.DocumentOfEngOfficeConfirmingSaveInfo;

            if (CurrentTaskCode == DocMeFileSaveInfoTaskCode)
            {
                DataTable dtWorkFlowState = WorkFlowStateManager.SelectByTableType(TableType, TableId);
                if (dtWorkFlowLastState.Rows.Count > 0)
                {
                    int FirstTaskCode = int.Parse(dtWorkFlowState.Rows[0]["TaskCode"].ToString());
                    int FirstNmcId = int.Parse(dtWorkFlowState.Rows[0]["NmcId"].ToString());
                    int FirstNmcIdType = int.Parse(dtWorkFlowState.Rows[0]["NmcIdType"].ToString());

                    if (FirstNmcIdType == 1)
                    {
                        if (FirstNmcId == Utility.GetCurrentUser_MeId())
                        {
                            FileManager.FindByCode(TableId);
                            if (FileManager.Count == 1)
                            {

                                DataTable dtNextTopTask = WorkFlowTaskManager.SelectNextTopSteps(TableType, DocMeFileSaveInfoTaskCode, CurrentWorkFlowCode);
                                if (dtNextTopTask.Rows.Count > 0)
                                {
                                    Session["SendBackDataTable_OffRq"] = dtNextTopTask;
                                    cmbSendBackTask.DataSource = dtNextTopTask;
                                    cmbSendBackTask.ValueField = "TaskId";
                                    cmbSendBackTask.TextField = "TaskName";
                                    cmbSendBackTask.DataBind();
                                    cmbSendBackTask.SelectedIndex = 0;
                                    PanelSaveSuccessfully.ClientVisible = false;
                                    PanelMain.ClientVisible = true;
                                }
                                else
                                {
                                    PanelSaveSuccessfully.ClientVisible = true;
                                    PanelMain.ClientVisible = false;
                                    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                                    lblInstitueWarning.Text = "عملیات بعد در جریان کار نامشخص است.";
                                }

                            }
                            else
                            {
                                PanelSaveSuccessfully.ClientVisible = true;
                                PanelMain.ClientVisible = false;
                                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                                lblInstitueWarning.Text = "اطلاعات مورد نظر توسط کاربر دیگری تغییر یافته است.";
                            }
                        }
                        else
                        {
                            PanelSaveSuccessfully.ClientVisible = true;
                            PanelMain.ClientVisible = false;
                            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                            lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
                        }
                    }
                    else
                    {
                        PanelSaveSuccessfully.ClientVisible = true;
                        PanelMain.ClientVisible = false;
                        lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                        lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
                    }
                }
                else
                {
                    PanelSaveSuccessfully.ClientVisible = true;
                    PanelMain.ClientVisible = false;
                    lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                    lblInstitueWarning.Text = "عملیاتی برای پرونده پروانه انتخاب شده انجام نشده است.";
                }
            }
            else
            {
                PanelSaveSuccessfully.ClientVisible = true;
                PanelMain.ClientVisible = false;
                lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
                lblInstitueWarning.Text = "امکان ارسال پرونده به مرحله بعد برای شما وجود ندارد.";
            }
        }
        else
        {
            PanelSaveSuccessfully.ClientVisible = true;
            PanelMain.ClientVisible = false;
            lblInstitueWarning.ForeColor = System.Drawing.Color.Red;
            lblInstitueWarning.Text = "عملیاتی برای پرونده پروانه انتخاب شده انجام نشده است.";
        }
    }
    protected void CallbackPanelWorkFlow_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
    {
        int focucedIndex = -1;
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            DataRow rowOff = CustomAspxDevGridView1.GetDataRow(CustomAspxDevGridView1.FocusedRowIndex);
            int EngOfId = (int)rowOff["EngOfId"];

            TSP.WebControls.CustomAspxDevGridView grid = (TSP.WebControls.CustomAspxDevGridView)CustomAspxDevGridView1.FindDetailRowTemplateControl(CustomAspxDevGridView1.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (grid != null)
            {
                focucedIndex = grid.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = grid.GetDataRow(focucedIndex);
                    int EOfId = (int)row["EOfId"];
                    int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
                    if (e.Parameter == "Send")
                    {
                        SendMemberFileDocToNextStep(EOfId, EngOfId);
                        grid.DataBind();
                    }
                    else
                    {
                        SelectSendBackTask(TableType, EOfId, EngOfId);
                    }
                }
                else
                {
                    PanelMain.ClientVisible = false;
                    PanelSaveSuccessfully.ClientVisible = true;
                    lblInstitueWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                    return;
                }
            }
            else
            {
                PanelMain.ClientVisible = false;
                PanelSaveSuccessfully.ClientVisible = true;
                lblInstitueWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                return;
            }
        }
        else
        {
            PanelMain.ClientVisible = false;
            PanelSaveSuccessfully.ClientVisible = true;
            lblInstitueWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
            return;
        }
    }
    protected void btnTracing_Click(object sender, EventArgs e)
    {
        if (CustomAspxDevGridView1.FocusedRowIndex > -1)
        {
            int TableId = -1;
            int TableType = (int)TSP.DataManager.TableCodes.EngOffFile;
            TSP.WebControls.CustomAspxDevGridView grid = (TSP.WebControls.CustomAspxDevGridView)CustomAspxDevGridView1.FindDetailRowTemplateControl(CustomAspxDevGridView1.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (grid != null)
            {
                int focucedIndex = grid.FocusedRowIndex;
                if (focucedIndex > -1)
                {
                    DataRow row = grid.GetDataRow(focucedIndex);
                    TableId = (int)row["EOfId"];
                }
                else
                {
                    this.DivReport.Visible = true;
                    this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                    return;
                }
            }
            else
            {
                this.DivReport.Visible = true;
                this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";
                return;

            }

            Response.Redirect("WorkFlowReport.aspx?TblType=" + Utility.EncryptQS(TableType.ToString()) + "&TblId=" + Utility.EncryptQS(TableId.ToString()));
        }
        else
        {
            this.DivReport.Visible = true;
            this.LabelWarning.Text = "لطفاً ابتدا یک درخواست را انتخاب نمائید";

        }
    }
    #endregion

    protected void CustomAspxDevGridView1_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "ParticipateLetterDate")
            e.Cell.Style["direction"] = "ltr";


        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)CustomAspxDevGridView1.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)CustomAspxDevGridView1.Columns["WFState"], "btnWFState");
            if (btnWFState != null)
            {
                if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
                {
                    btnWFState.ToolTip = "تعریف نشده";
                    btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
                    return;
                }

                if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFStart.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFInProcess.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
                }
                else
                {
                }
            }
        }
    }
    protected void CustomAspxDevGridViewRequest_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
    {
        if (e.DataColumn.FieldName == "CreateDate" || e.DataColumn.FieldName == "FileNo")
            e.Cell.Style["direction"] = "ltr";


        if (e.DataColumn.FieldName == "TaskId")
        {
            DevExpress.Web.ASPxGridView GridViewRequest = (DevExpress.Web.ASPxGridView)CustomAspxDevGridView1.FindDetailRowTemplateControl(CustomAspxDevGridView1.FocusedRowIndex, "CustomAspxDevGridViewRequest");
            if (GridViewRequest == null)
                return;
            DevExpress.Web.ASPxImage btnWFState = (DevExpress.Web.ASPxImage)GridViewRequest.FindRowCellTemplateControl(e.VisibleIndex, (DevExpress.Web.GridViewDataColumn)GridViewRequest.Columns["WFState"], "btnWFState");
            if (btnWFState != null)
            {
                if (Utility.IsDBNullOrNullValue(e.GetValue("WFTaskType")))
                {
                    btnWFState.ToolTip = "تعریف نشده";
                    btnWFState.ImageUrl = "~/Images/WFUnNounState.png";
                    return;
                }

                if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.StartingTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFStart.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.MeddleTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFInProcess.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.ConfirmingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFConfirmAndEnd.png";
                }
                else if (int.Parse(e.GetValue("WFTaskType").ToString()) == (int)TSP.DataManager.WorkFlowTaskType.RejectingingAndEndProccessTask)
                {
                    btnWFState.ToolTip = e.GetValue("TaskName").ToString();
                    btnWFState.ImageUrl = "~/Images/WFREjectAndEnd.png";
                }
                else
                {
                }
            }
        }
    }
    protected void CustomAspxDevGridView1_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "ParticipateLetterDate")
            e.Editor.Style["direction"] = "ltr";
    }
    protected void CustomAspxDevGridViewRequest_AutoFilterCellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
    {
        if (e.Column.FieldName == "CreateDate" || e.Column.FieldName == "FileNo")
            e.Editor.Style["direction"] = "ltr";
    }
    protected void CustomAspxDevGridView1_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridViewDetailRowEventArgs e)
    {
        CustomAspxDevGridView1.FocusedRowIndex = e.VisibleIndex;

    }
    protected void CustomAspxDevGridView1_FocusedRowChanged(object sender, EventArgs e)
    {

        if (CustomAspxDevGridView1.FocusedRowIndex != -1 && CustomAspxDevGridView1.SettingsDetail.ShowDetailRow)
            CustomAspxDevGridView1.DetailRows.ExpandRow(CustomAspxDevGridView1.FocusedRowIndex);
    }
    protected void CustomAspxDevGridView1_PageIndexChanged(object sender, EventArgs e)
    {
        CustomAspxDevGridView1.JSProperties["cpIsPostBack"] = 1;
    }
    protected void CustomAspxDevGridViewRequest_BeforePerformDataSelect(object sender, EventArgs e)
    {
        Session["EngOfficeId"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        int Index = CustomAspxDevGridView1.FindVisibleIndexByKeyValue((int)(sender as ASPxGridView).GetMasterRowKeyValue());
        CustomAspxDevGridView1.FocusedRowIndex = Index;
    }
    protected void CustomAspxDevGridView1_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != GridViewRowType.Data)
            return;
        if (e.GetValue("IsConfirm") != null)
        {
            if (e.GetValue("IsConfirm").ToString() == "0")
                e.Row.BackColor = System.Drawing.Color.LightSteelBlue;
        }
    }
}
